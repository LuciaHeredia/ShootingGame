using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindGun : MonoBehaviour
{
    public GameObject frame;
    public Text findGunText;

    private bool holdText;

    void Start()
    {
        holdText = true;

        frame.gameObject.SetActive(true); // show frame text
        findGunText.gameObject.SetActive(true); // show text
        // wait 5 seconds
        StartCoroutine(StartStopper());
    }

    void Update()
    {
        if(!holdText)
        {
            frame.gameObject.SetActive(false); // turn off frame text
            findGunText.gameObject.SetActive(false); // turn off text
        }
    }

    public IEnumerator StartStopper()
    {
        yield return new WaitForSeconds(3);
        holdText = false;
    }
}
