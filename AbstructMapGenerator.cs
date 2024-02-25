using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructMapGenerator : MonoBehaviour
{
    [SerializeField] protected TilMapVisulaizer tilmapVisulaizer = null;
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero ;

    public void GeneratedMap(){

        tilmapVisulaizer.clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
