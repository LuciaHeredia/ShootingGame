using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorMotion : MonoBehaviour
{

    private Animator animator;
    private AudioSource doorSound;

    void Start() // Start is called before the first frame update
    {
        animator = GetComponent<Animator>();
        doorSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("OpenSlideDoor", true);
        doorSound.PlayDelayed(0.5f);

    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("OpenSlideDoor", false);
        doorSound.PlayDelayed(0.5f);

    }

    void Update() // Update is called once per frame
    {
        
    }
}
