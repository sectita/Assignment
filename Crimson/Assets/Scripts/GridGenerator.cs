using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject GridTile;
    [SerializeField] private int gridx;
    [SerializeField] private int gridz;
    [SerializeField] private float gridOffset;
    Vector3 gridOrigin = Vector3.zero;

    void Start()
    {
        SpawnGrid();
        //SpawnObsticleGrid();
    }

    private void SpawnGrid()
    {
        for(int i = 0; i < gridx; i++)
        {
            for (int j = 0; j < gridz; j++)
            {
                Vector3 generatePos = new Vector3(i * gridOffset, 0, j * gridOffset) + gridOrigin;
                GameObject clone = Instantiate(GridTile, generatePos, Quaternion.identity);
                clone.transform.SetParent(this.transform);
            }
        }
    }
    #region For Obsticle
    //[SerializeField] private GameObject GridObsticle;
    //[SerializeField] private int obsticlex;
    //[SerializeField] private int obsticlez;
    //[SerializeField] private float obsticleOffset;
    //Vector3 obsticleOrigin = Vector3.zero;
    //[SerializeField] private int obsticleY;

    //void SpawnObsticleGrid()
    //{
    //    for (int k = 0; k < obsticlex; k++)
    //    {
    //        for (int l = 0; l < obsticlez; l++)
    //        {
    //            Vector3 generatePosY = new Vector3(k * obsticleOffset, obsticleY, l * obsticleOffset) + obsticleOrigin;
    //            GameObject clone1 = Instantiate(GridObsticle, generatePosY, Quaternion.identity);
    //            clone1.transform.SetParent(this.transform);
    //        }
    //    }
    //}
    #endregion
}
