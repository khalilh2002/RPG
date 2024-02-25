using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CorridorFirstMapGenerator : simpleWalkMapGenerator
{
   [SerializeField] private int corridorLenght = 20 , corridorCount = 5 ;
   [SerializeField] [Range(0.1f , 1)] private float roomPercente;
    
    protected override void RunProceduralGeneration()
    {
        CorridorFirstMapGeneration();
    }
    //route to room
    private void CorridorFirstMapGeneration()
    {
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPosition = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors =  createCorridor(floorPosition , potentialRoomPosition);
        

        HashSet<Vector2Int> roomPositions = createRoom(potentialRoomPosition);

        List<Vector2Int> deadEnds = FindDeadEnd(floorPosition);
        createDeadEndRooms(deadEnds , roomPositions);

        floorPosition.UnionWith(roomPositions);

        //triple the size of the corridors 
        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = insert3x3(corridors[i]).ToList<Vector2Int>();
            floorPosition.UnionWith(corridors[i]);
        }

        tilmapVisulaizer.paintFloorTiles(floorPosition);
        WallGenerator.createWalls(floorPosition,tilmapVisulaizer);
    }

    private HashSet<Vector2Int> insert3x3(List<Vector2Int> corrider)
    {
        HashSet<Vector2Int> corriderFix = new HashSet<Vector2Int>();
        for (int i = 1; i < corrider.Count; i++)
        {
            for (int x = 1; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    corriderFix.Add(corrider[i-1]+new Vector2Int(x,y));
                }
            }
        }
        return corriderFix;
    }

    private void createDeadEndRooms(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach (var deadEndPosition in deadEnds)
        {
            if (roomPositions.Contains(deadEndPosition)==false)
            {
                var newRoomDeadEnd = RandomWalk(simpleRandomWalkParametre,deadEndPosition);
                roomPositions.UnionWith(newRoomDeadEnd);
            }
        }
        
    }

    private List<Vector2Int> FindDeadEnd(HashSet<Vector2Int> floorPosition)
    {
        List<Vector2Int> deadEnd = new List<Vector2Int>();

        foreach (var position in floorPosition)
        {
            int neighbors = 0 ;
            //search every direction to find neighors (floor)
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if (floorPosition.Contains(position + direction)==true)
                {
                    neighbors++;
                }
            }
            //dead end have only one neighbor 
            if(neighbors==1){
                deadEnd.Add(position);
            }
            
        }
        
        return deadEnd;
    }

    //Room
    private HashSet<Vector2Int> createRoom(HashSet<Vector2Int> potentialRoomPosition)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateOut =Mathf.RoundToInt(potentialRoomPosition.Count * roomPercente);
        //just we give a random rooms positions to roomtocreate list not nesseary
        List<Vector2Int> roomsToCreate = potentialRoomPosition.OrderBy(x => Guid.NewGuid()).Take(roomToCreateOut).ToList<Vector2Int>();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RandomWalk(simpleRandomWalkParametre,roomPosition); 
            roomPositions.UnionWith(roomFloor);
        }   

        return roomPositions;
    }

    private List<List<Vector2Int>> createCorridor(HashSet<Vector2Int> floorPosition , HashSet<Vector2Int> potentialRoomPosition )
    {
        var currentPosition = startPosition;
        potentialRoomPosition.Add(currentPosition);

        List<List<Vector2Int>> corridors_all = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var corrodor = GenerateMapAlgorithm.RandomWalkCorridor(currentPosition,corridorLenght);
            //Stack (Pile) -> pop(depile)
            
            corridors_all.Add(corrodor);

            currentPosition = corrodor [ corrodor.Count - 1 ] ; 
            potentialRoomPosition.Add(currentPosition);
            floorPosition.UnionWith(corrodor);
        }

        return corridors_all;
    }
}
