using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObsticleData : ScriptableObject
{
    public string ObsticleName;
    public GameObject ObsticlePrefab;
    public int gridx;
    public int gridz;
    public float gridOffset;
    public float ObsticleY;

    public abstract void GenerateObsticle();
}
