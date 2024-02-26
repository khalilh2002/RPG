using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class RoomFirstMapGenerator : simpleWalkMapGenerator
{
    [SerializeField]
    private int minRoomWidth = 4 , minRoomHeight = 4 ;
    [SerializeField]
    private int mapWidth = 20 , mapHeight = 20;
    [SerializeField][Range(0,10)]
    private int offset = 1 ;
    //private bool randomWalkRooms = false ;

    protected override void RunProceduralGeneration()
    {
        createRooms();
    }

    private void createRooms()
    {
        var roomlist = GenerateMapAlgorithm.BinarySpacePartition( new BoundsInt((Vector3Int)startPosition , new Vector3Int(mapWidth,mapHeight,0))
                                                                    ,minRoomWidth ,minRoomHeight);
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = createSimpleRooms(roomlist);
        tilmapVisulaizer.paintFloorTiles(floor);
        WallGenerator.createWalls(floor,tilmapVisulaizer);
    }

    private HashSet<Vector2Int> createSimpleRooms(List<BoundsInt> roomlist)
    {   HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomlist)
        {
            for (int column = offset; column < room.size.x - offset; column++)
            {
                for (int row = offset; row < room.size.y - offset; row++){
                    Vector2Int position =  (Vector2Int)room.min + new Vector2Int(column , row );
                    floor.Add(position);
                }
            }
        } 

        return floor;
    }
}
