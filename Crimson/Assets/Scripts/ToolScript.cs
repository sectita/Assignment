using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ToolScript
{
    [MenuItem("Tools/Materials/Assign Tile material")]
    public static void AssignTilematerial()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material mat = Resources.Load<Material>("Tile");

        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().material = mat;
        }
    }


    [MenuItem("Tools/Materials/Assign Tile material to NonTile")]
    public static void AssignTilematerialObsticle()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("NonTile");
        Material mat = Resources.Load<Material>("NonTile");

        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().material = mat;
        }
    }


    [MenuItem("Tools/Scripts/Assign Tile Script")]
    public static void AssignTileScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        
        foreach (GameObject t in tiles)
        {
            t.AddComponent<Tile>();
        }
    }


    [MenuItem("Tools/GridGeneratorTab")]
    public static void GridGeneratorTab()
    {
        GridTabEditor.GridGeneratorWindow();
    }
}

