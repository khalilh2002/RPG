using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenerateMapAlgorithm
{
    //Radom Walk algo
    //hash is like an array but doesnt allow duplicate
    //hash allows just vector2Dint
    public static HashSet<Vector2Int> simpleRandomWalk(Vector2Int startPosition , int walkLenght){

        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var previousPosition = startPosition;

        for(int i=0 ; i< walkLenght ; i++){

            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path; 
    }

    public static List<Vector2Int> RandomWalkCorridor (Vector2Int startPosition , int corridorLenght){
        
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var curentPosition = startPosition;
        corridor.Add(curentPosition);

        for (int i = 0; i < corridorLenght; i++)
        {
            curentPosition = curentPosition + direction ;
            corridor.Add(curentPosition);
        }
        return corridor;
    }

    /*---------------------- NEED OPTIIZATION--------------------------*/
    // Binary Space Partition
    // BoundsInt (Espace)
    public static List<BoundsInt> BinarySpacePartition(BoundsInt spaceToSplit , int minWidth , int minHeight){
        List<BoundsInt> roomList = new List<BoundsInt>();
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt>();

        roomQueue.Enqueue(spaceToSplit);

        while (roomQueue.Count>0)
        {
            var room = roomQueue.Dequeue();
            if (Random.value < 0.5f) //splite verticualy
            {
                if (room.size.y >= minHeight && room.size.x >= minWidth)
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth  , roomQueue , room);

                    }else if(room.size.y >= minHeight * 2){

                        SplitHorizontally( minHeight , roomQueue , room);

                    }else{
                        roomList.Add(room);
                    }
                }

            }else{ //splite horizontaly
                if(room.size.y >= minHeight * 2){

                    SplitHorizontally(minHeight , roomQueue , room);
                    
                }else if (room.size.x >= minWidth * 2)
                {
                    SplitVertically(minWidth  , roomQueue , room);

                }else{
                    roomList.Add(room);
                }
            }
        }
        return roomList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        int splitX = Random.Range(1,room.size.x);
        BoundsInt room1 = new BoundsInt(room.min , new  Vector3Int(splitX , room.size.y , room.size.z) );
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + splitX , room.min.y , room.min.z) ,
                                        new  Vector3Int(room.size.x - splitX , room.size.y , room.size.z) );
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
        
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
         int splitY = Random.Range(1,room.size.y);
        BoundsInt room1 = new BoundsInt(room.min , new  Vector3Int(room.size.x , splitY , room.size.z) );
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + splitY , room.min.z) ,
                                        new  Vector3Int(room.size.x, room.size.y - splitY , room.size.z) );
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }
}

//class Direction2d used to  find random  direction of the algorithm to folow like up , down , left and right
public static class Direction2D{
    //create list type vector2Int and intialize with "up,down,right,left" vector2int type (list = {up , down , left , right})
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>{
        new Vector2Int(0,1), //up
        new Vector2Int(0,-1), //down
        new Vector2Int(1,0), //right
        new Vector2Int(-1,0) //left
    }; 

    public static Vector2Int GetRandomCardinalDirection(){

        return cardinalDirectionList[Random.Range(0,cardinalDirectionList.Count)];
    }
}
