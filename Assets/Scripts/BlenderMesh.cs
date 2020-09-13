using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderMesh : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    //Vector3[] originalVertices;
  

    // Start is called before the first frame update
    void Start()
    { 
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        //originalVertices = mesh.vertices;
    }


    public void TransformShape(TransMatrix matrix)
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            //print(mesh.vertices[i].x + " " + mesh.vertices[i].y + " " + mesh.vertices[i].z);
            float x = vertices[i].x;
            float y = vertices[i].y;
            float z = vertices[i].z;
            vertices[i] = new Vector3(x * matrix.matrix[0].x + y * matrix.matrix[0].y + z * matrix.matrix[0].z,
                                      x * matrix.matrix[1].x + y * matrix.matrix[1].y + z * matrix.matrix[1].z,
                                      x * matrix.matrix[2].x + y * matrix.matrix[2].y + z * matrix.matrix[2].z);
        }

        mesh.vertices = vertices;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

}
