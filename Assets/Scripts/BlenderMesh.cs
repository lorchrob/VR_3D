using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class BlenderMesh : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] targetVertices;
    Vector3[] differences;

    double counter;
    public float transformTime = 0.2f; // Length of time for transformations;

    Rigidbody rb;

    // Initialize all of the vertices;
    void Start()
    { 
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        targetVertices = mesh.vertices;
        differences = new Vector3[vertices.Length];
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Linearly move each vertex to the target vertex.
        if (counter <= transformTime)
        {
            rb.useGravity = false;
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                vertices[i] += differences[i] * Time.deltaTime / transformTime;
            }
            mesh.vertices = vertices;
            GetComponent<MeshCollider>().sharedMesh = mesh;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            counter += Time.deltaTime;
        }
        else
        {
            rb.useGravity = true;
        }
    }

    // Set the target vertices based on the transform being applied.
    public void TransformShape(Vector3[] matrix)
    {
        counter = 0;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float x = targetVertices[i].x;
            float y = targetVertices[i].y;
            float z = targetVertices[i].z;
            targetVertices[i] = new Vector3(x * matrix[0].x + y * matrix[0].y + z * matrix[0].z,
                                      x * matrix[1].x + y * matrix[1].y + z * matrix[1].z,
                                      x * matrix[2].x + y * matrix[2].y + z * matrix[2].z);
            differences[i] = targetVertices[i] - vertices[i];
        }

    }

}
