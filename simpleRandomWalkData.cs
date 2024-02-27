using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="simpleRandomWalkParametres_",menuName ="PCG/simpleRandomWalkData")]

public class simpleRandomWalkData : ScriptableObject
{
    public int iterations = 10 , walkLenght = 10;
    public bool startRandomEachIteration = true;
}
