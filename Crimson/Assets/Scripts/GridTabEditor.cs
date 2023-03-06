using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridTabEditor : EditorWindow
{
    private const float width = 300;
    private const float height = 500;
    private static GridTabEditor gridTabEditor = default;

    public List<ObsticleData> list = new List<ObsticleData>();

    public static void GridGeneratorWindow()
    {
        if (gridTabEditor == null)
        {
            gridTabEditor = GetWindow<GridTabEditor>();
            gridTabEditor.minSize = new Vector2(width, height);
            gridTabEditor.Show();
        }
        else
        {
            gridTabEditor.Focus();
        }
    }

    private void OnGUI()
    {
        GridGenerator();
        Grid();
        Repaint();
    }

    GameObject grid = default;
    private void Grid()
    {
        Vector3 gridOrigin = Vector3.zero;
        foreach (ObsticleData item in list)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("List");
            item.ObsticlePrefab = (GameObject)EditorGUILayout.ObjectField(item.ObsticlePrefab, typeof(GameObject), true);
            GUILayout.EndHorizontal();
            if (ObsticleGroup == null)
            {
                ObsticleGroup = new GameObject("GridTab");
            }
            for (int i = 0; i < item.gridx; i++)
            {
                for (int j = 0; j < item.gridz; j++)
                {
                    Vector3 generatePos = new Vector3(i * item.gridOffset, item.ObsticleY, j * item.gridOffset) + gridOrigin;
                    GameObject clone = Instantiate(item.ObsticlePrefab, generatePos, Quaternion.identity);
                    clone.transform.SetParent(grid.transform);
                }
            }
        }
    }


    private void GridGenerator()
    {
        EditorGUILayout.LabelField("Grid Generator", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Object");
        GridObsticle = (GameObject)EditorGUILayout.ObjectField(GridObsticle, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("obsticlex");
        obsticlex = EditorGUILayout.IntField(obsticlex);

        GUILayout.Label("obsticlez");
        obsticlez = EditorGUILayout.IntField(obsticlez);

        GUILayout.Label("obsticleOffset");
        obsticleOffset = EditorGUILayout.FloatField(obsticleOffset);

        GUILayout.Label("obsticley");
        obsticleY = EditorGUILayout.IntField(obsticleY);
        GUILayout.EndHorizontal();


        if (GUILayout.Button("GenerateGrid"))
        {
            if (ObsticleGroup == null)
            {
                ObsticleGroup = new GameObject("Obsticle Group");
            }
            for (int k = 0; k < obsticlex; k++)
            {
                for (int l = 0; l < obsticlez; l++)
                {
                    Vector3 generatePosY = new Vector3(k * obsticleOffset, obsticleY, l * obsticleOffset) + obsticleOrigin;
                    GameObject clone1 = Instantiate(GridObsticle, generatePosY, Quaternion.identity);
                    clone1.transform.SetParent(ObsticleGroup.transform);
                }
            }
        }
        if (GUILayout.Button("DeleteGrid"))
        {
            DestroyImmediate(ObsticleGroup);
        }
        ToggleFun();
    }
    GameObject ObsticleGroup = default;
    private GameObject GridObsticle;
    private int obsticlex;//width "K"
    private int obsticlez;//height "l"
    private float obsticleOffset;
    Vector3 obsticleOrigin = Vector3.zero;
    private int obsticleY;
    /// <summary>
    /// ////////////////////////////////////////
    /// </summary>
    
    GameObject Object0, Object1, Object2, Object3, Object4;

    bool[] a = new bool[5] { false, false, false, false, false };
    bool[] b = new bool[3] { false, false, false };

    private void ToggleFun()
    {

        b[0] = EditorGUILayout.Toggle("b0", b[0]);
        b[1] = EditorGUILayout.Toggle("b1", b[1]);
        b[2] = EditorGUILayout.Toggle("b2", b[2]);

        //a0
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Object0");
        Object0 = (GameObject)EditorGUILayout.ObjectField(Object0, typeof(GameObject), true);
        a[0] = EditorGUILayout.Toggle("Toggle0", a[0]);
        if (!a[0])
        {
            Object0.gameObject.SetActive(false);
        }
        else
        {
            Object0.gameObject.SetActive(true);
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(5);

        //a1
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Object1");
        Object1 = (GameObject)EditorGUILayout.ObjectField(Object1, typeof(GameObject), true);
        a[1] = EditorGUILayout.Toggle("Toggle1", a[1]);
        if (!a[1])
        {
            Object1.gameObject.SetActive(false);
        }
        else
        {
            Object1.gameObject.SetActive(true);
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(5);

        //a2
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Object2");
        if (!a[2])
        {
            Object2.gameObject.SetActive(false);
        }
        else
        {
            Object2.gameObject.SetActive(true);
        }

        Object2 = (GameObject)EditorGUILayout.ObjectField(Object2, typeof(GameObject), true);
        a[2] = EditorGUILayout.Toggle("Toggle2", a[2]);
        GUILayout.EndHorizontal();
        GUILayout.Space(5);

        //a3
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Object3");
        if (!a[3])
        {
            Object3.gameObject.SetActive(false);
        }
        else
        {
            Object3.gameObject.SetActive(true);
        }

        Object3 = (GameObject)EditorGUILayout.ObjectField(Object3, typeof(GameObject), true);
        a[3] = EditorGUILayout.Toggle("Toggle3", a[3]);
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
        //a4
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Object4");
        if (!a[4])
        {
            Object4.gameObject.SetActive(false);
        }
        else
        {
            Object4.gameObject.SetActive(true);
        }

        Object4 = (GameObject)EditorGUILayout.ObjectField(Object4, typeof(GameObject), true);
        a[4] = EditorGUILayout.Toggle("Toggle4", a[4]);
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
    }
}

