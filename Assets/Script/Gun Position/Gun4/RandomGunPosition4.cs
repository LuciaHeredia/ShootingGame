using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGunPosition4 : MonoBehaviour
{
    
    void Start() 
    {
        float dx, dz, dy;

        dy = 131.72f;
        dx = Random.Range(-82.48f,227.41f);
        dz = Random.Range(139.3f,141.5f);
        
        Vector3 positions = new Vector3(dx,dy,dz);
        transform.position = positions;
    }
    
}
