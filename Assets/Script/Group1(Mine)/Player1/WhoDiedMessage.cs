using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhoDiedMessage : MonoBehaviour
{
    public GameObject npc1;
    public GameObject npc2;
    public GameObject player2;

    private Animator animator1;
    private Animator animator2;
    private Animator animator3;

    public GameObject frame;
    public Text whoDiedText;

    private bool stpMsgNpc1;
    private bool stpMsgNpc2;
    private bool stpMsgPlayer2;

    private bool holdText;

    void Start()
    {
        stpMsgNpc1 = false;
        stpMsgNpc2 = false;
        stpMsgPlayer2 = false;

        holdText = true;

        animator1 = npc1.GetComponent<Animator>();
        animator2 = npc2.GetComponent<Animator>();
        animator3 = player2.GetComponent<Animator>();
    }

    void Update()
    {
        if(animator1.GetCurrentAnimatorStateInfo(0).IsName("Falling Back Death")
           && !animator2.GetCurrentAnimatorStateInfo(0).IsName("Falling Forward Death")
           && !stpMsgNpc1)
        {
            frame.gameObject.SetActive(true); // show frame text
            whoDiedText.text = "NPC1 dead.";
            whoDiedText.gameObject.SetActive(true); // show text
            StartCoroutine(StartStopper()); // wait 2 seconds
            stpMsgNpc1 = true;
        }

        if(!animator1.GetCurrentAnimatorStateInfo(0).IsName("Falling Back Death")
           && animator2.GetCurrentAnimatorStateInfo(0).IsName("Falling Forward Death")
           && !stpMsgNpc2)
        {
            frame.gameObject.SetActive(true); // show frame text
            whoDiedText.text = "NPC2 dead.";
            whoDiedText.gameObject.SetActive(true); // show text
            StartCoroutine(StartStopper()); // wait 2 seconds
            stpMsgNpc2 = true;
        }

        // Player 2 dies
        if(animator3.GetCurrentAnimatorStateInfo(0).IsName("Death From The Back")
           && !stpMsgPlayer2)
        {
            frame.gameObject.SetActive(true); // show frame text
            whoDiedText.text = "Player2 dead.";
            whoDiedText.gameObject.SetActive(true); // show text
            StartCoroutine(StartStopper()); // wait 2 seconds
            stpMsgPlayer2 = true;
        }

    }
    
    public IEnumerator StartStopper()
    {
        yield return new WaitForSeconds(2);
        frame.gameObject.SetActive(false); // turn off frame text
        whoDiedText.gameObject.SetActive(false); // turn off text
    }
}
