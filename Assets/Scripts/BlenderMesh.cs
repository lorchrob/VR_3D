using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderMesh : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] targetVertices;

    // Start is called before the first frame update
    void Start()
    { 
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        targetVertices = mesh.vertices;
    }

    void Update()
    {


        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            vertices[i].x += (targetVertices[i].x - vertices[i].x) * Time.deltaTime * 4;
            vertices[i].y += (targetVertices[i].y - vertices[i].y) * Time.deltaTime * 4;
            vertices[i].z += (targetVertices[i].z - vertices[i].z) * Time.deltaTime * 4;

            mesh.vertices = vertices;
            GetComponent<MeshCollider>().sharedMesh = mesh;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }


    public void TransformShape(Vector3[] matrix)
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float x = targetVertices[i].x;
            float y = targetVertices[i].y;
            float z = targetVertices[i].z;
            targetVertices[i] = new Vector3(x * matrix[0].x + y * matrix[0].y + z * matrix[0].z,
                                      x * matrix[1].x + y * matrix[1].y + z * matrix[1].z,
                                      x * matrix[2].x + y * matrix[2].y + z * matrix[2].z);
        }

    }

}
