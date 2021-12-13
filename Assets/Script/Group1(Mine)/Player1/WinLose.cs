using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class WinLose : MonoBehaviour
{
    public GameObject endPanel;
    public TextMeshProUGUI panelText;

    public GameObject npc1;
    public GameObject npc2;
    public GameObject gun1;
    public GameObject gun2;
    public GameObject player2;
    private Animator animator1;
    private Animator animator2;
    private NavMeshAgent agent1;
    private NavMeshAgent agent2;
    private NavMeshAgent agent3;

    public GameObject crossHairHorizontal;
    public GameObject crossHairVertical;

    private bool stopAnnouncing;
    private bool goToMenu;
    private bool imDead;

    void Start()
    {
        stopAnnouncing = false;
        goToMenu = false;
        imDead = false;

        animator1 = npc1.GetComponent<Animator>();
        animator2 = npc2.GetComponent<Animator>();

        agent1 = npc1.GetComponent<NavMeshAgent>();
        agent2 = npc2.GetComponent<NavMeshAgent>();
        agent3 = player2.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
         if(!stopAnnouncing)
        {
            // np1 and np2 are dead - I WIN
            if(animator1.GetCurrentAnimatorStateInfo(0).IsName("Falling Back Death") &&
               animator2.GetCurrentAnimatorStateInfo(0).IsName("Falling Forward Death"))
            {
                crossHairHorizontal.gameObject.SetActive(false); // turn off
                crossHairVertical.gameObject.SetActive(false); // turn off
                panelText.text = "YOU WON"; // result in final panel
                goToMenu = true;
            }
            
            if (imDead) // - I LOSE
            {
                crossHairHorizontal.gameObject.SetActive(false); // turn off
                crossHairVertical.gameObject.SetActive(false); // turn off

                panelText.text = "YOU LOST"; // result in final panel
                goToMenu = true; 
            }

            if(goToMenu)
            {
                endPanel.gameObject.SetActive(true); // go to final panel
                agent1.isStopped = true; // stop movement of npc1
                agent2.isStopped = true; // stop movement of npc2
                agent3.isStopped = true; // stop movement of player2
                stopAnnouncing = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == npc1.gameObject.name) //near target: npc1
        {
            if(gun1.gameObject.activeSelf)
                imDead = true;
        }
       
       if(other.gameObject.name == npc2.gameObject.name) //near target: npc2
        {
            if(gun2.gameObject.activeSelf)
                imDead = true;
        }
    }

}
