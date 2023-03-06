using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BFSMove : MonoBehaviour, IBFSMove
{
    public bool turn = false;
    
    List<Tile> selectableTileList = new List<Tile>();
    GameObject[] tiles;
    Stack<Tile> path = new Stack<Tile>();
    protected Tile currenetTile;

    public bool moving = false;
    public int TileToTileMove;
    public float moveSpeed;

    float halfHeight = 0;

    Vector3 moveVelocity = new Vector3();
    Vector3 moveDirection = new Vector3();


    protected void init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = GetComponent<Collider>().bounds.extents.y;

        TurnManager.AddUnit(this);
    }

    public void GetCurrentTile()
    {
        currenetTile = GetTargetTile(gameObject);
        currenetTile.CurrentPos = true;
    }

    public Tile GetTargetTile(GameObject tar)
    {
        RaycastHit hitinfo;
        Tile tile = null;
        if (Physics.Raycast(tar.transform.position, -Vector3.up, out hitinfo, 1))
        {
            tile = hitinfo.collider.GetComponent<Tile>();
        }
        return tile;
    }

    public void CalculateAdjecencyList(Tile target)
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.CheckNeighbourTiles(target);
        }
    }

    //BFS
    public void FindSelectableTiles()
    {
        CalculateAdjecencyList(null);
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currenetTile);
        currenetTile.VisitedTile = true;
        currenetTile.ParentTile = null;

        while (process.Count > 0)
        {
            Tile t = process.Dequeue();
            selectableTileList.Add(t);
            t.SelectableTile = true;

            if(t.DistanceTile < TileToTileMove)
            {
                foreach (Tile tile in t.adjecencyTileList)
                {
                    if (!tile.VisitedTile)
                    {
                        tile.ParentTile = t;
                        tile.VisitedTile = true;
                        tile.DistanceTile = 1 + tile.DistanceTile;
                        process.Enqueue(tile);
                    }
                }
            }
        }

    }

    public void MoveToTarget(Tile tile)
    {
        path.Clear();
        tile.TargetPos = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.ParentTile;
        }
    }

    public void MoveUnits()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            //calcuate unit position to targetpos
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculatingHeading(target);
                SetHorizontalVelocity();


                transform.forward = moveDirection;
                transform.position += moveDirection * Time.deltaTime;
            }
            else
            {
                //tille center reached
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectedTiles();
            moving = false;
            TurnManager.EndTurn();
        }
    }

    protected void RemoveSelectedTiles()
    {
        if (currenetTile != null)
        {
            currenetTile.CurrentPos = false;
            currenetTile = null;
        }
        foreach (Tile tile in selectableTileList)
        {
            tile.ResetTilePositions();
        }
        selectableTileList.Clear();
    }

    void CalculatingHeading(Vector3 target)
    {
        moveDirection = target - transform.position;
        moveDirection.Normalize();
    }

    void SetHorizontalVelocity()
    {
        moveVelocity = moveDirection * moveSpeed;
    }

    //TurnBase
    public void BeginTurn()
    {
        turn = true;
    }

    public void EndTurn()
    {
        turn = false;
    }


    //Enemy Path A*
    public Tile actualTargetTile;

    public Tile EndTileFind(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();
        Tile next = t.ParentTile;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.ParentTile;
        }
        if (tempPath.Count <= TileToTileMove)
        {
            return t.ParentTile;
        }

        Tile endTile = null;
        for (int i = 0; i <= TileToTileMove; i++)
        {
            endTile = tempPath.Pop();
        }
        return endTile;
    }

    public void PathFind(Tile target)
    {
        CalculateAdjecencyList(target);
        GetCurrentTile();

                                                 //A* uses 2 lists
        List<Tile> openList = new List<Tile>();  // not been process 
        List<Tile> closeList = new List<Tile>(); // has been process to find path

        openList.Add(currenetTile);
        //currenetTile.ParentTile = null;

        currenetTile.h = Vector3.Distance(currenetTile.transform.position, target.transform.position);
        currenetTile.f = currenetTile.h;

        while (openList.Count > 0)
        {
            Tile t = LowestFCost(openList);

            closeList.Add(t);
            if (t == target)
            {
                actualTargetTile = EndTileFind(t);
                MoveToTarget(actualTargetTile);
                return;
            }
            foreach (Tile tile in t.adjecencyTileList)
            {
                if (closeList.Contains(tile))
                {

                }
                else if (openList.Contains(tile))
                {
                    float gDist = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    if (gDist < tile.g)
                    {
                        tile.ParentTile = t;
                        tile.g = gDist;
                        tile.f = tile.g = tile.h;
                    }
                }
                else
                {
                    tile.ParentTile = t;
                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;
                    openList.Add(tile);
                }
            }

        }

    }

    public Tile LowestFCost(List<Tile> openList)
    {
        Tile LowestF = openList[0];

        foreach (Tile list in openList)
        {
            if (list.f < LowestF.f)
            {
                LowestF = list;
            }
        }
        openList.Remove(LowestF);
        return LowestF;
    }
}