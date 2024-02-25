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
