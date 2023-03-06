using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleManager : MonoBehaviour
{
    public List<ObsticleData> list = new List<ObsticleData>();

    Vector3 gridOrigin = Vector3.zero;


    void Start()
    {
        //generate obsticle
        
        foreach (ObsticleData item in list)
        {
            for (int i = 0; i < item.gridx; i++)
            {
                for (int j = 0; j < item.gridz; j++)
                {
                    Vector3 generatePos = new Vector3(i * item.gridOffset, item.ObsticleY, j * item.gridOffset) + gridOrigin;
                    GameObject clone = Instantiate(item.ObsticlePrefab, generatePos, Quaternion.identity);
                    clone.transform.SetParent(this.transform);  
                }
            }
        }
    }
}
