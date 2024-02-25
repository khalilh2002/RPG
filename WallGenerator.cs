using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void createWalls(HashSet<Vector2Int> floorPosition , TilMapVisulaizer tilMapVisulaizer){
        var basicWallPosition = FindWallsInDirections(floorPosition , Direction2D.cardinalDirectionList);
        foreach (var position in basicWallPosition)
        {
            tilMapVisulaizer.paintWallTiles(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPosition, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPosition = new HashSet<Vector2Int>();
        foreach (var position in floorPosition)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if (floorPosition.Contains(neighbourPosition) == false)
                {
                    wallPosition.Add(neighbourPosition);
                }
            }
        }
        return wallPosition;
    }
}
