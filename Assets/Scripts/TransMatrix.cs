using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Matrix", menuName = "Transformation Matrix")]
public class TransMatrix : ScriptableObject
{
    
    public Vector3[] matrix = { new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1) };

    public Vector3[] invertMatrix = { new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1) };

    public void calcInverse()
    {

        float det = matrix[0].x * (matrix[2].z * matrix[1].y - matrix[1].z * matrix[2].y)
            - matrix[0].y * (matrix[1].x * matrix[2].z - matrix[1].z * matrix[2].x)
            + matrix[0].z * (matrix[2].y * matrix[1].x - matrix[1].y * matrix[2].x);

        invertMatrix[0].x = (1 / det) * (matrix[2].z * matrix[1].y - matrix[1].z * matrix[2].y);
        invertMatrix[0].y = (1 / det) * (matrix[2].y * matrix[0].z - matrix[0].y * matrix[2].z);
        invertMatrix[0].z = (1 / det) * (matrix[0].y * matrix[1].z - matrix[0].z * matrix[1].y);

        invertMatrix[1].x = (1 / det) * (matrix[1].z * matrix[2].x - matrix[1].x * matrix[2].z);
        invertMatrix[1].y = (1 / det) * (matrix[0].x * matrix[2].z - matrix[0].z * matrix[2].x);
        invertMatrix[1].z = (1 / det) * (matrix[0].z * matrix[1].x - matrix[0].x * matrix[1].z);

        invertMatrix[2].x = (1 / det) * (matrix[1].x * matrix[2].y - matrix[1].y * matrix[2].x);
        invertMatrix[2].y = (1 / det) * (matrix[0].y * matrix[2].x - matrix[0].x * matrix[2].y);
        invertMatrix[2].z = (1 / det) * (matrix[0].x * matrix[1].y - matrix[0].y * matrix[1].x);

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(invertMatrix[i].x.ToString() + ", " + invertMatrix[i].y.ToString() + ", " + invertMatrix[i].z.ToString());
        }
    }

}
