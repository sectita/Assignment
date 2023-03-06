using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BFSMove, IBFSMove
{
    
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
            FindSelectableTiles();
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Tile")
                    {
                        Tile t = hit.collider.GetComponent<Tile>();
                        if (t.SelectableTile)
                        {
                            MoveToTarget(t);
                        }
                    }
                }
            }
        }
        else
        {
            MoveUnits();
        }
    }
}
