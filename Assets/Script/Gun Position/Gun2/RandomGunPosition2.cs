using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGunPosition2 : MonoBehaviour
{
    
    void Start() 
    {
        float dx=0.0f, dz=0.0f, dy=0.0f;
        int rndSection, rndSectionFountain, rndSectionForest;

        // 0 - around fountain
        // 1 - in the forest
        rndSection = Random.Range(0,2);
        if(rndSection==0) // around fountain
        {
            dy = 131.9979f;
            rndSectionFountain = Random.Range(0,4); // around the fountain
            if(rndSectionFountain==0)
            {
                dx = Random.Range(151.3f,196.8f);
                dz = Random.Range(-85.05f,-70.7f);
            }
            else if(rndSectionFountain==1)
            {
                dx = Random.Range(186.9f,196.8f);
                dz = Random.Range(-125.3f,-70.35f);
            }
            else if(rndSectionFountain==2)
            {
                dx = Random.Range(152.1f,196.8f);
                dz = Random.Range(-124.8f,-110.4f);
            }
            else // 3
            {
                dx = Random.Range(152.5f,161.14f);
                dz = Random.Range(-125.3f,-70.35f);
            }

        }
        else // in the forest
        {
            dy = 131.72f;
            rndSectionForest = Random.Range(0,2); // around the forest
            if(rndSectionForest==0)
            {
                dx = Random.Range(182.7f,229.8f);
                dz = Random.Range(-163.96f,-142.12f);
            }
            else if(rndSectionForest==1)
            {
                dx = Random.Range(216.9f,226.9f);
                dz = Random.Range(-132.78f,-42.93f);
            }
            
        }

        Vector3 positions = new Vector3(dx,dy,dz);
        transform.position = positions;
    }
    
}
