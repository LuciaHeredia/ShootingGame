using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMotion : MonoBehaviour
{
    private float speed, angularSpeed;
    private float rotationAboutX=0, rotationAboutY=90;

    private CharacterController controller;

    public GameObject PlayerCamera;
    public GameObject myGun;
    private AudioSource stepSound;
    public AudioSource pickUp;

    public GameObject cube1;
    public GameObject gun1;
    public GameObject frame1;
    public Text gunText1;
    private bool takeGun1;

    public GameObject cube2;
    public GameObject gun2;
    public GameObject frame2;
    public Text gunText2;
    private bool takeGun2;

    public GameObject cube3;
    public GameObject gun3;
    public GameObject frame3;
    public Text gunText3;
    private bool takeGun3;

    public GameObject cube4;
    public GameObject gun4;
    public GameObject frame4;
    public Text gunText4;
    private bool takeGun4;

    public TextMeshProUGUI panelText;
    
    void Start() // Start is called before the first frame update
    {
        speed = 16;
        angularSpeed = 100;
        controller = GetComponent<CharacterController>();
        stepSound = GetComponent<AudioSource>();
        takeGun1 = false;
        takeGun2 = false;
    }
    
    void Update() // Update is called once per frame
    {
        float dx, dy, dz;
        dy = -1; // is a gravity

       // if(panelText.isActiveAndEnabled) // if end-text enabled, disable walking
       //     controller.enabled = false; 

        // Player rotation
        rotationAboutY += Input.GetAxis("Mouse X")*angularSpeed*Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, rotationAboutY, 0);

        // Camera rotation
        rotationAboutX -= Input.GetAxis("Mouse Y")*angularSpeed*Time.deltaTime;
        PlayerCamera.transform.localEulerAngles = new Vector3(rotationAboutX,0,0);

        // motion after rotation
        dx = Input.GetAxis("Horizontal"); // -1 or 0 or +1
        dx*=speed*Time.deltaTime;
        dz = Input.GetAxis("Vertical"); // -1 or 0 or +1
        dz*=speed*Time.deltaTime;

        
        // Motion using CharacterController
        Vector3 motion = new Vector3(dx,dy,dz); // motion is defined in Local coordinates
        motion = transform.TransformDirection(motion); // now motion is in Global coordinates        

        controller.Move(motion); // must recieve Vector3 in Globl coordinates

        if(dx<-0.01 || dx>0.01 || dz<-0.01 || dz>0.01)
        {
            if(!stepSound.isPlaying)
                stepSound.Play();
        }
        

        if(takeGun1 && gun1.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                gun1.gameObject.SetActive(false); 
                myGun.gameObject.SetActive(true); 
                pickUp.Play();
                gunText1.gameObject.SetActive(false); // turn off text
                frame1.gameObject.SetActive(false); // turn off frame text
                takeGun1 = false;
            }
        }
        if(takeGun1 && !gun1.gameObject.activeSelf)
        {
            gunText1.gameObject.SetActive(false); // turn off text
            frame1.gameObject.SetActive(false); // turn off frame text
        }

        if(takeGun2 && gun2.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                gun2.gameObject.SetActive(false); 
                myGun.gameObject.SetActive(true); 
                pickUp.Play();
                gunText2.gameObject.SetActive(false); // turn off text
                frame2.gameObject.SetActive(false); // turn off frame text
                takeGun2 = false;
            }
        }
        if(takeGun2 && !gun2.gameObject.activeSelf)
        {
            gunText2.gameObject.SetActive(false); // turn off text
            frame2.gameObject.SetActive(false); // turn off frame text
        }

        if(takeGun3 && gun3.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                gun3.gameObject.SetActive(false); 
                myGun.gameObject.SetActive(true); 
                pickUp.Play();
                gunText3.gameObject.SetActive(false); // turn off text
                frame3.gameObject.SetActive(false); // turn off frame text
                takeGun3 = false;
            }
        }
        if(takeGun3 && !gun3.gameObject.activeSelf)
        {
            gunText3.gameObject.SetActive(false); // turn off text
            frame3.gameObject.SetActive(false); // turn off frame text
        }

        if(takeGun4 && gun4.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                gun4.gameObject.SetActive(false); 
                myGun.gameObject.SetActive(true); 
                pickUp.Play();
                gunText4.gameObject.SetActive(false); // turn off text
                frame4.gameObject.SetActive(false); // turn off frame text
                takeGun4 = false;
            }
        }
        if(takeGun4 && !gun4.gameObject.activeSelf)
        {
            gunText4.gameObject.SetActive(false); // turn off text
            frame4.gameObject.SetActive(false); // turn off frame text
        }

    }

     private void OnTriggerEnter(Collider other)
    {
        if(!myGun.gameObject.activeSelf)
        {
            if(other.gameObject.name == cube1.gameObject.name) 
            {
                if(gun1.gameObject.activeSelf)
                {
                    gunText1.gameObject.SetActive(true); // show text
                    frame1.gameObject.SetActive(true); // show frame text
                    takeGun1 = true;
                }
                else
                {
                    gunText1.gameObject.SetActive(false); // turn off text
                    frame1.gameObject.SetActive(false); // turn off frame text
                }
            }
            if(other.gameObject.name == cube2.gameObject.name) 
            {
                if(gun2.gameObject.activeSelf)
                {
                    gunText2.gameObject.SetActive(true); // show text
                    frame2.gameObject.SetActive(true); // show frame text
                    takeGun2 = true;
                }
                else
                {
                    gunText2.gameObject.SetActive(false); // turn off text
                    frame2.gameObject.SetActive(false); // turn off frame text
                }
            }
            if(other.gameObject.name == cube3.gameObject.name) 
            {
                if(gun3.gameObject.activeSelf)
                {
                    gunText3.gameObject.SetActive(true); // show text
                    frame3.gameObject.SetActive(true); // show frame text
                    takeGun3 = true;
                }
                else
                {
                    gunText3.gameObject.SetActive(false); // turn off text
                    frame3.gameObject.SetActive(false); // turn off frame text
                }
            }
            if(other.gameObject.name == cube4.gameObject.name) 
            {
                if(gun4.gameObject.activeSelf)
                {
                    gunText4.gameObject.SetActive(true); // show text
                    frame4.gameObject.SetActive(true); // show frame text
                    takeGun4 = true;
                }
                else
                {
                    gunText4.gameObject.SetActive(false); // turn off text
                    frame4.gameObject.SetActive(false); // turn off frame text
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == cube1.gameObject.name) 
        {
            gunText1.gameObject.SetActive(false); // turn off text
            frame1.gameObject.SetActive(false); // turn off frame text
            takeGun1 = false;
        }

        if(other.gameObject.name == cube2.gameObject.name) 
        {
            gunText2.gameObject.SetActive(false); // turn off text
            frame2.gameObject.SetActive(false); // turn off frame text
            takeGun2 = false;
        }

        if(other.gameObject.name == cube3.gameObject.name) 
        {
            gunText3.gameObject.SetActive(false); // turn off text
            frame3.gameObject.SetActive(false); // turn off frame text
            takeGun3 = false;
        }

        if(other.gameObject.name == cube4.gameObject.name) 
        {
            gunText4.gameObject.SetActive(false); // turn off text
            frame4.gameObject.SetActive(false); // turn off frame text
            takeGun4 = false;
        }
    }

}
