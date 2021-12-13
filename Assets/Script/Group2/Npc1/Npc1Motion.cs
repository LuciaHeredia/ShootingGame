using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Npc1Motion : MonoBehaviour
{
    private Animator animator;
    private Animator animator1;
    private Animator animator2;
    private Animator animatorNpc2;
    private NavMeshAgent agent;

    public GameObject player1; // target(me)
    public GameObject player2; // target
    public GameObject player2Gun;
    public GameObject npc2;
    public GameObject npc2Gun;
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
        animator1 = player1.GetComponent<Animator>();
        animator2 = player2.GetComponent<Animator>();
        animatorNpc2 = npc2.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        hasGun = false;
        hasTarget = true;
        nearTargetPlayer1 = false;
        nearTargetPlayer2 = false;
        noTargets = false;
        firstRndGun = Random.Range(0,4); // choose one of 4 guns
        rndTarget = Random.Range(0,2); // choose one of 2 targets
    }

    void Update()
    {
        /*** IF NOT DEAD ***/
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Back Death"))
        {   
            /*** Path to Gun ***/
            if(!hasGun)
            { // if he does not have a gun
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

            if(myGun.gameObject.activeSelf && !npc2Gun.gameObject.activeSelf &&
                !animatorNpc2.GetCurrentAnimatorStateInfo(0).IsName("Falling Forward Death")) 
            {   // i have a gun but npc2 hasnt found a gun and shes not dead -> join her
                animator.SetInteger("state",2); // walking with gun position
                agent.SetDestination(npc2.transform.position); // follow target
            }

            if(!noTargets && 
                (animatorNpc2.GetCurrentAnimatorStateInfo(0).IsName("Falling Forward Death") ||
                npc2Gun.gameObject.activeSelf)) // new target + npc2 found a gun
            {
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
                    animator.SetInteger("state",1); // holding gun position
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
                    animator.SetInteger("state",1); // holding gun position
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
                    animator.SetInteger("state",1); // holding gun position
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
                    animator.SetInteger("state",1); // holding gun position
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
}



