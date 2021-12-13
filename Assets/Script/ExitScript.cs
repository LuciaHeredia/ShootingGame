using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnExitPressed()
    {
        UnityEngine.Debug.LogError("Exit Game");
        Application.Quit();
    }

}
