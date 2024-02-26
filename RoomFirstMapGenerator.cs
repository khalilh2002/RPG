using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random ;

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

        //list of centers of rooms
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomlist)
        {
            Vector2Int center = (Vector2Int)Vector3Int.RoundToInt(room.center); 
            roomCenters.Add(center);
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilmapVisulaizer.paintFloorTiles(floor);
        WallGenerator.createWalls(floor,tilmapVisulaizer);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0,roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);
        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestTo(currentRoomCenter , roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter , closest);
           
            currentRoomCenter = closest ;
            corridors.UnionWith(newCorridor);
        }
        

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var postion = currentRoomCenter; //start point;
        corridor.Add(postion);
        while (postion.y != destination.y)
        {
            if (postion.y > destination.y)
            {
                postion += Vector2Int.down;
            }else{
                postion += Vector2Int.up;
            }
            corridor.Add(postion);
        }
        while (postion.x != destination.x)
        {
            if (postion.x > destination.x)
            {
                postion += Vector2Int.left;
            }else{
                postion += Vector2Int.right;
            }
            corridor.Add(postion);
        }
        

        return corridor;
    }

    private Vector2Int FindClosestTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float distance_test = Vector2Int.Distance( position , currentRoomCenter);
            if (distance > distance_test)
            {
                distance = distance_test ;
                closest = position;
            }
        }
        return closest ; 
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
