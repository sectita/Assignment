using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public bool CurrentPos = false;
    public bool TargetPos = false;
    public bool SelectableTile = false;
    public bool WalkableTile = true;

    public bool VisitedTile = false;
    public Tile ParentTile = null;
    public int DistanceTile = 0;

    public float f = 0;
    public float g = 0;
    public float h = 0;


    public List<Tile> adjecencyTileList = new List<Tile>();

    void Start()
    {
        Cordinates = FindObjectOfType<Text>();
    }


    void Update()
    {

        if (CurrentPos)
        {
            GetComponent<Renderer>().material.color = Color.grey;
        }
        else if (TargetPos)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (SelectableTile)
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }

        MouseUnitPosition();
    }

    public void ResetTilePositions()
    {
        adjecencyTileList.Clear();
        CurrentPos = false;
        TargetPos = false;
        SelectableTile = false;

        VisitedTile = false;
        ParentTile = null;
        DistanceTile = 0;

        f = 0;
        g = 0;
        h = 0;
    }
    public void CheckNeighbourTiles(Tile target)
    {
        ResetTilePositions();

        CheckTile(Vector3.forward, target);
        CheckTile(-Vector3.forward, target);
        CheckTile(Vector3.right, target);
        CheckTile(-Vector3.right, target);
    }
    public void CheckTile(Vector3 dir, Tile target)
    {
        Vector3 extents = new Vector3(0.25f, 0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + dir, extents);
        foreach (Collider col in colliders)
        {
            Tile tile = col.GetComponent<Tile>();
            if (tile != null && tile.WalkableTile)
            {
                RaycastHit hitinfo;
                if(!Physics.Raycast(tile.transform.position, Vector3.up, out hitinfo, 0.75f) || (tile == target))
                {
                    adjecencyTileList.Add(tile);
                }
            }
        }
    }


    #region MouseUnitPosition
    private Text Cordinates;

    void MouseUnitPosition()
    {
        Vector3 worldPosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo) && hitinfo.transform.tag == "Tile" && Cordinates != null)
        {
            worldPosition = hitinfo.point;
            worldPosition = Vector3Int.RoundToInt(worldPosition);
            Cordinates.text = " Grid Cordinates : x,y,z" + worldPosition.ToString();
            //print(worldPosition);
        }
    }
    #endregion


    #region Mouse_Hover
    //private void OnMouseEnter()
    //{
    //    GetComponent<Renderer>().material.color = Color.blue;
    //}
    //private void OnMouseExit()
    //{
    //    GetComponent<Renderer>().material.color = Color.white;
    //}
    #endregion
}
