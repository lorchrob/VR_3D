using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Matrix", menuName = "Transformation Matrix")]
public class TransMatrix : ScriptableObject
{
    
    public Vector3[] matrix = { new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1) };


}
