using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGunPosition1 : MonoBehaviour
{
    
    void Start() 
    {
        float dx, dz, dy = 131.9979f;
        int rndSection;

        // 0 - near Neighberhod Parking
        // 1 - on the road
        rndSection = Random.Range(0,2);

        if(rndSection==0){
            dx = Random.Range(-33.6f,-81.6f);
            dz = Random.Range(-16.5f,70.5f);
        }
        else
        {
            dx = Random.Range(-33.2f,-83.5f);
            dz = Random.Range(-171.8f,-26.1f);
        }

        Vector3 positions = new Vector3(dx,dy,dz);
        transform.position = positions;
    }
    
}
