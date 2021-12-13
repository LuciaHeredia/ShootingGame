using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGunPosition3 : MonoBehaviour
{
    
    void Start() 
    {
        float dx=0.0f, dz=0.0f, dy = 131.72f;
        int rndSection;

        // 0 - main street in the neighborhood
        // 1 - secondary road1
        // 2 - secondary road2
        // 3 - secondary road3
        rndSection = Random.Range(0,4);

        if(rndSection==0)
        {
            dx = Random.Range(175.8f,-21.2f);
            dz = Random.Range(44.4f,53.7f);
        }
        else if(rndSection==1)
        {
            dx = Random.Range(80.01f,70.7f);
            dz = Random.Range(-15.01f,35.01f);
        }
        else if(rndSection==2)
        {
            dx = Random.Range(108.1f,95.1f);
            dz = Random.Range(70.25f,115.6f);
        }
        else
        {
            dx = Random.Range(191.75f,183.55f);
            dz = Random.Range(-8.7f,118.4f);
        }

        Vector3 positions = new Vector3(dx,dy,dz);
        transform.position = positions;
    }
    
}
