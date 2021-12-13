using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhoAttacksScript : MonoBehaviour
{
    public GameObject npc1;
    public GameObject npc2;
    public GameObject player2;

    public GameObject myGun;
    public GameObject player2Gun;
    public GameObject npc1Gun;
    public GameObject npc2Gun;

    public GameObject redFrame;
    public GameObject greenFrame;
    public Text weAttackText;
    public Text theyAttackText;

    private bool holdText;
    private bool stopAnnouncing;

    private int count2Announces;

    void Start()
    {
        count2Announces = 0;
        holdText = true;
        stopAnnouncing = false;
    }

    void Update()
    {
        if(!stopAnnouncing)
        {
            // if other group has 2 guns before my group has 2 guns
            // show text "YOU ARE UNDER ATTACKED"
            if((npc1Gun.gameObject.activeSelf && npc2Gun.gameObject.activeSelf) &&
                ((!myGun.gameObject.activeSelf && !player2Gun.gameObject.activeSelf) ||
                (!myGun.gameObject.activeSelf && player2Gun.gameObject.activeSelf) ||
                (myGun.gameObject.activeSelf && !player2Gun.gameObject.activeSelf)))
            {
                redFrame.gameObject.SetActive(true); // show frame text
                theyAttackText.gameObject.SetActive(true); // show text
                // wait 3 seconds
                StartCoroutine(StartStopper());
                if(!holdText)
                {
                    redFrame.gameObject.SetActive(false); // turn off frame text
                    theyAttackText.gameObject.SetActive(false); // turn off text
                    stopAnnouncing = true;
                }
                if(count2Announces!=2)
                    holdText = true;
            }
            
            if((myGun.gameObject.activeSelf && player2Gun.gameObject.activeSelf) &&
                ((!npc1Gun.gameObject.activeSelf && !npc2Gun.gameObject.activeSelf) ||
                (!npc1Gun.gameObject.activeSelf && npc2Gun.gameObject.activeSelf) ||
                (npc1Gun.gameObject.activeSelf && !npc2Gun.gameObject.activeSelf)))
            {   // show text "ATTACK ENEMY"
                greenFrame.gameObject.SetActive(true); // turn on frame text
                weAttackText.gameObject.SetActive(true); // show text
                // wait 3 seconds
                StartCoroutine(StartStopper());
                if(!holdText)
                {
                    greenFrame.gameObject.SetActive(false); // turn off frame text
                    weAttackText.gameObject.SetActive(false); // turn off text
                    stopAnnouncing = true;
                }
                if(count2Announces!=2)
                    holdText = true;
            }

        }
    }

    public IEnumerator StartStopper()
    {
        yield return new WaitForSeconds(3);
        holdText = false;
        count2Announces  = count2Announces + 1;
    }
}
