using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BFSMove,IBFSMove
{
    GameObject target;
    void Start()
    {
        init();
    }

    
    void Update()
    {
        if (!turn)
        {
            return;
        }
        if (!moving)
        {
            GetNearPath();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.TargetPos = true;
        }
        else
        {
            MoveUnits();
        }
    }

    private void GetNearPath()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject near = null;
        float distance = Mathf.Infinity;

        foreach (GameObject objTarget in targets)
        {
            float dis = Vector3.Distance(transform.position, objTarget.transform.position);
            if (dis < distance)
            {
                distance = dis;   //closer
                near = objTarget; // each obj
            }
        }

        target = near;
    }

    private void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        PathFind(targetTile);
    }
}
