using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Npc2Motion : MonoBehaviour
{
    private Animator animator;
    private Animator animator1;
    private Animator animator2;
    private NavMeshAgent agent;
    
    public GameObject leaderPlayer; 
    public GameObject player1; // target(me)
    public GameObject player2; // target
    public GameObject player2Gun;
    public GameObject npc1;
    public GameObject gun1;
    public GameObject cube1;
    public GameObject gun2;
    public GameObject cube2;
    public GameObject gun3;
    public GameObject cube3;
    public GameObject gun4;
    public GameObject cube4;
    public GameObject myGun;

    private bool hasGun;
    private bool nearLeader;
    private bool nearTargetPlayer1;
    private bool nearTargetPlayer2;
    private bool hasTarget;
    private bool noTargets;

    public AudioSource fireSound;
    public TextMeshProUGUI panelText;

    private int firstRndGun;
    private int numGun;
    private int rndTarget;
    private int[] arrGuns = new int[] { 1, 1, 1, 1};
    private int[] arrTargets = new int[] { 1, 1};
    
    void Start()
    {
        animator = GetComponent<Animator>();
        animator1 = npc1.GetComponent<Animator>();
        animator2 = player2.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        nearLeader = false;
        hasGun = false;
        hasTarget = true;
        nearTargetPlayer1 = false;
        nearTargetPlayer2 = false;
        noTargets = false;
        firstRndGun = Random.Range(0,4); // choose one of 4 guns
        rndTarget = Random.Range(1,2); // choose one of 2 targets 
    }

    void Update()
    {
        /*** Path to Gun ***/
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Forward Death"))
        { // if not dead
            if(!hasGun)
            {  // if she does not have a gun
                if(firstRndGun<4)
                {
                    numGun = firstRndGun;
                    firstRndGun=4;
                }
                else
                {
                    numGun=0;
                    while(arrGuns[numGun]!=1 && numGun<4)
                        numGun++;
                    if(numGun==4)
                        numGun=0;
                }
                gunPath(numGun); // gun is available path
            }

            if(!animator1.GetCurrentAnimatorStateInfo(0).IsName("Falling Back Death") &&
                !nearLeader && myGun.gameObject.activeSelf)
            { // if i have a gun but im not near the leader and he is not dead
                agent.isStopped = false;
                animator.SetInteger("state",2); // walking holding gun position
                agent.SetDestination(leaderPlayer.transform.position); // follow main player
            }

            if(!noTargets && animator1.GetCurrentAnimatorStateInfo(0).IsName("Falling Back Death"))
            {   // no leader to follow
                 /*** Path to Target ***/
                if(myGun.gameObject.activeSelf && (!nearTargetPlayer2 || !nearTargetPlayer1))
                { 
                    animator.SetInteger("state",2); // walking with gun position

                    if(!hasTarget)
                    {
                        if(arrTargets[0]==1) 
                            rndTarget = 0; // 0 - target: player1
                        if(arrTargets[1]==1) 
                            rndTarget = 1; // 1 - target: player2
                        hasTarget = true;
                    }
                    targetPath(rndTarget);
                }
            }
        }
        else  /*** IF DEAD ***/
            agent.isStopped = true;
    }

    private void targetPath(int numTarget)
    {
        if(numTarget==0) 
        {   // target: player1
            agent.SetDestination(player1.transform.position); // follow target
        }
        
        if(numTarget==1 && !animator2.GetCurrentAnimatorStateInfo(0).IsName("Death From The Back")) 
        {   // target: player2
            agent.SetDestination(player2.transform.position); // follow target
        }
    }

    private void gunPath(int numOfGun)
    {
        if(numOfGun==0 && gun1.gameObject.activeSelf)
            agent.SetDestination(gun1.transform.position);
        else if(numOfGun==1 && gun2.gameObject.activeSelf)
            agent.SetDestination(gun2.transform.position);
        else if(numOfGun==2 && gun3.gameObject.activeSelf)
            agent.SetDestination(gun3.transform.position);
        else if(numOfGun==3 && gun4.gameObject.activeSelf)
            agent.SetDestination(gun4.transform.position);
            
        hasGun=true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!myGun.gameObject.activeSelf)
        {
            if(other.gameObject.name == cube1.gameObject.name) 
            {
                if(gun1.gameObject.activeSelf)
                {
                    hasGun=true;
                    animator.SetInteger("state",2); // walking holding gun 
                    gun1.gameObject.SetActive(false); // hide gun
                    myGun.gameObject.SetActive(true);// hold gun
                }
                else
                {
                    arrGuns[numGun]=0; // gun not available
                    hasGun=false;
                }
            }
            if(other.gameObject.name == cube2.gameObject.name) 
            {
                if(gun2.gameObject.activeSelf)
                {
                    hasGun=true;
                    animator.SetInteger("state",2); // walking holding gun 
                    gun2.gameObject.SetActive(false); // hide gun
                    myGun.gameObject.SetActive(true);// hold gun
                }
                else
                {
                    arrGuns[numGun]=0; // gun not available
                    hasGun=false;
                }
            }
            if(other.gameObject.name == cube3.gameObject.name) 
            {
                if(gun3.gameObject.activeSelf)
                {
                    hasGun=true;
                    animator.SetInteger("state",2); // walking holding gun 
                    gun3.gameObject.SetActive(false); // hide gun
                    myGun.gameObject.SetActive(true);// hold gun
                }
                else
                {
                    arrGuns[numGun]=0; // gun not available
                    hasGun=false;
                }
            }
            if(other.gameObject.name == cube4.gameObject.name) 
            {
                if(gun4.gameObject.activeSelf)
                {
                    hasGun=true;
                    animator.SetInteger("state",2); // walking holding gun 
                    gun4.gameObject.SetActive(false); // hide gun
                    myGun.gameObject.SetActive(true);// hold gun
                }
                else
                {
                    arrGuns[numGun]=0; // gun not available
                    hasGun=false;
                }
            }
        }

        /* Holding gun and following leader player */
        if(other.gameObject.name == leaderPlayer.gameObject.name) 
        {
            if(myGun.gameObject.activeSelf) // is holding his gun already
            {
                agent.isStopped = true;
                nearLeader = true;
                animator.SetInteger("state",1); // holding gun position
            }
        }

        /*** Path to Target: PLAYER2 ***/
        if(other.gameObject.name == player2.gameObject.name) // near target: player 2
        {
            if(!panelText.isActiveAndEnabled && myGun.gameObject.activeSelf &&
                !animator2.GetCurrentAnimatorStateInfo(0).IsName("Death From The Back"))
            {   // near target and he is alive
                targetPath(1);
                animator.SetInteger("state",1); // holding gun position
                fireSound.Play();
                player2Gun.gameObject.SetActive(false);// hide gun
                animator2.SetInteger("state",3);
                arrTargets[1]=0; // target dead
                hasTarget = false; // find new target
            }
        }

        /*** Path to Target: PLAYER1 ***/
        if(other.gameObject.name == player1.gameObject.name) // near target: player 1
        {
            if(myGun.gameObject.activeSelf && !panelText.isActiveAndEnabled)
            {   // if end-text enabled, disable shooting
                targetPath(0);
                animator.SetInteger("state",1); // holding gun position
                agent.isStopped = true; // stop movement of npc1
                fireSound.Play();
                arrTargets[0]=0; // target dead
                hasTarget = false; // find new target
                // all targets dead -> stop movement
                noTargets = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        /* Holding gun and following leader player */
        if(other.gameObject.name == leaderPlayer.gameObject.name) 
        {
            if(myGun.gameObject.activeSelf) // is holding his gun already
                nearLeader = false;
        }
    }

}
