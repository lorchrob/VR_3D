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
    Hashtable transApplied = new Hashtable();

    int numApplied = 0;
    public int maxApplied = 5;
    double counter;
    public float transformTime = 0.2f; // Length of time for transformations;
    public Vector3 maxDist;

    Rigidbody rb;

    // Initialize all of the vertices;
    void Start()
    { 
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        targetVertices = mesh.vertices;
        differences = new Vector3[vertices.Length];
        if (tag == "Transformable")
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        // Linearly move each vertex to the target vertex.
        if (counter <= transformTime)
        {
            maxDist = Vector3.zero;
            if (tag == "Transformable")
            {
                rb.useGravity = false;
            }
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                vertices[i] += differences[i] * Time.deltaTime / transformTime;
                calcMaxes(i);
            }

            mesh.vertices = vertices;
            GetComponent<MeshCollider>().sharedMesh = mesh;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            counter += Time.deltaTime;
        }
        else
        {
            if (tag == "Transformable")
            {
                rb.useGravity = true;
            }
        }
    }

    void calcMaxes(int i)
    {
        
        if (vertices[i].x > maxDist.x)
        {
            maxDist.x = vertices[i].x;
        }
        if (vertices[i].y > maxDist.y)
        {
            maxDist.y = vertices[i].y;
        }
        if (vertices[i].z > maxDist.z)
        {
            maxDist.z = vertices[i].z;
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

    public void TransformShapeRestricted(Vector3[] matrix, bool inverse, string name) {
        if (transApplied.ContainsKey(name)) 
        {
            // We make sure that the count doesn't extend past magnitude 5
            // in positive or negative direction in order to limit the number
            // of transformations that can be applied
            int count = (int) transApplied[name];
            if (!inverse && count >= 0) {
                if (numApplied == maxApplied) return;
                numApplied++;
                transApplied.Remove(name);
                transApplied.Add(name, count + 1);
            }
            else if (!inverse && count < 0)
            {
                numApplied--;
                transApplied.Remove(name);
                transApplied.Add(name, count + 1);
            }
            else if (inverse && count > 0)
            {
                numApplied--;
                transApplied.Remove(name);
                transApplied.Add(name, count - 1);
            }
            else if (inverse && count <= 0)
            {
                if (numApplied == maxApplied) return;
                numApplied++;
                transApplied.Remove(name);
                transApplied.Add(name, count - 1);
            }
        } 

        else 
        {
            // If the hash table doesn't contain the key, we add it
            if (numApplied == maxApplied) return;
            numApplied++;
            if (inverse)
            {
                transApplied.Add(name, -1);
            }
            else
            {
                transApplied.Add(name, 1);
            }
        }
        
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


