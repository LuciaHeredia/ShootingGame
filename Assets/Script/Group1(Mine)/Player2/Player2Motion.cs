using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Player2Motion : MonoBehaviour
{
    private Animator animator;
    private Animator animatorNpc1;
    private Animator animatorNpc2;
    private NavMeshAgent agent;

    public GameObject leaderPlayer; // (me)
    public GameObject npc1; // target
    public GameObject npc2; // target
    public GameObject npc1Gun;
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
    private bool nearLeader;
    private bool nearTargetNpc1;
    private bool nearTargetNpc2;
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
        animatorNpc1 = npc1.GetComponent<Animator>();
        animatorNpc2 = npc2.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        nearLeader = false;
        hasGun = false;
        nearTargetNpc1 = false;
        nearTargetNpc2 = false;
        noTargets = false;
        firstRndGun = Random.Range(0,4); // choose one of 4 guns
        rndTarget = Random.Range(1,2); // choose one of 2 targets    
    }

    void Update()
    {
        /*** Path to Gun ***/
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Death From The Back"))
        { // if not dead
            if(!hasGun)
            {   
                if(firstRndGun<4)
                {
                    numGun = firstRndGun;
                    firstRndGun=4;
                }
                else
                {
                    numGun=0;
                    while(numGun<4 && arrGuns[numGun]!=1)
                        numGun++;
                    if(numGun==4)
                        numGun=0;
                }
                gunPath(numGun); // gun is available path
            }    

            if(!nearLeader && myGun.gameObject.activeSelf)
            { // follow leader
                agent.isStopped = false;
                animator.SetInteger("state",2); // walking holding gun position
                agent.SetDestination(leaderPlayer.transform.position); // follow main player
            }

        }
        else  /*** IF DEAD ***/
            agent.isStopped = true;
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
                    animator.SetInteger("state",2); // walking holding gun position
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

        /* Holding gun and following leader player(me) */
        if(other.gameObject.name == leaderPlayer.gameObject.name) 
        {
            if(myGun.gameObject.activeSelf) // is holding his gun already
            {
                agent.isStopped = true;
                nearLeader = true;
                animator.SetInteger("state",1); // holding gun position
            }
        }

        /*** Path to Target: NPC2 ***/
        if(other.gameObject.name == npc2.gameObject.name) // near target: npc 2
        {
            if(!panelText.isActiveAndEnabled && myGun.gameObject.activeSelf &&
                !animatorNpc2.GetCurrentAnimatorStateInfo(0).IsName("Falling Forward Death"))
            {   // holding gun + near target and he is alive
                animator.SetInteger("state",1); // holding gun position
                fireSound.Play();
                npc2Gun.gameObject.SetActive(false);// hide gun
                animatorNpc2.SetInteger("state",3);
                arrTargets[1]=0; // target dead
            }
        }

        /*** Path to Target: NPC1 ***/
        if(other.gameObject.name == npc1.gameObject.name) // near target: npc 1
        {
            if(!panelText.isActiveAndEnabled && myGun.gameObject.activeSelf &&
                !animatorNpc1.GetCurrentAnimatorStateInfo(0).IsName("Falling Back Death"))
            {   // holding gun + near target and he is alive
                animator.SetInteger("state",1); // holding gun position
                fireSound.Play();
                arrTargets[0]=0; // target dead
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        /* Holding gun and following leader player(me) */
        if(other.gameObject.name == leaderPlayer.gameObject.name) 
        {
            if(myGun.gameObject.activeSelf) // is holding his gun already
                nearLeader = false;
        }

    }
    
}
