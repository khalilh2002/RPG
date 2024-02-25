using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class simpleWalkMapGenerator : AbstructMapGenerator
{
    [SerializeField]protected simpleRandomWalkData simpleRandomWalkParametre;

    protected override void RunProceduralGeneration(){

        HashSet<Vector2Int> floorPosition = RandomWalk(simpleRandomWalkParametre , startPosition);
        tilmapVisulaizer.clear();
        tilmapVisulaizer.paintFloorTiles(floorPosition);
        WallGenerator.createWalls(floorPosition,tilmapVisulaizer);
    }

    protected HashSet<Vector2Int> RandomWalk(simpleRandomWalkData randomWalkGeneration , Vector2Int position)
    {
        var currentPostion = position;
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkGeneration.iterations; i++)
        {
           var path = GenerateMapAlgorithm.simpleRandomWalk(currentPostion,randomWalkGeneration.walkLenght);
            floorPosition.UnionWith(path);

            if(randomWalkGeneration.startRandomEachIteration){
                currentPostion = floorPosition.ElementAt(Random.Range(0,floorPosition.Count));
            }
        }
        return floorPosition;
    }


}
