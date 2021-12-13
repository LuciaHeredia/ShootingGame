using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shoothing : MonoBehaviour
{
    public AudioSource fireSound;

    public GameObject gun;
    public GameObject camera;
    public GameObject target1;

    public GameObject npc1;
    public GameObject npcGun1;
    public GameObject npc2;
    public GameObject npcGun2;

    private Animator animator1;
    private Animator animator2;

    public TextMeshProUGUI panelText;

    void Start()
    {
        animator1 = npc1.GetComponent<Animator>();
        animator2 = npc2.GetComponent<Animator>();
    }

    void Update()
    {
        if(!panelText.isActiveAndEnabled) // if end-text enabled, disable shooting
        {
            if(Input.GetKeyDown(KeyCode.Space) && gun.gameObject.activeSelf) // shooting
            {
                RaycastHit hit;

                if(Physics.Raycast(camera.transform.position,camera.transform.forward,out hit)) // shot place
                {
                    target1.transform.position = hit.point;
                    target1.gameObject.SetActive(true); // show bullet
                    fireSound.Play();

                    // check if the npc was injured
                    if(npc1.transform.gameObject==hit.transform.gameObject)
                    {
                        npcGun1.gameObject.SetActive(false); // hide gun
                        animator1.SetInteger("state",3);
                    }

                    if(npc2.transform.gameObject==hit.transform.gameObject)
                    {
                        npcGun2.gameObject.SetActive(false); // hide gun
                        animator2.SetInteger("state",3);
                    }
                    
                }
            }
        }
    }

}
