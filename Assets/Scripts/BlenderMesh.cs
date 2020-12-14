using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

/* This script, added to a gameobject, allows the gameobject to be transformed
by the linear transformation cannon (controlled by the player) */
public class BlenderMesh : MonoBehaviour
{
    Mesh mesh; // This object's mesh
    Vector3[] vertices; // Current vertices in the mesh
    Vector3[] startingVertices; // Vertices at the beginning of the transformation
    Vector3[] targetVertices; // Target vertices of the transformation
    Vector3[] differences; // Differences between vertices and target vertices

    List<string> transApplied = new List<string>(); // Keep track of the types of transformations applied

    public int maxApplied = 5; // Maximum number of transformations that can be applied to the object
    double counter; // Keeps track of how long since a transformation has been applied
    public float transformTime = 0.5f; // Length of time for transformations;
    public Vector3 maxDist; // Keeps track of the largest position values (x, y, and z) in the vertices,
                            // to be used for rendering the axes that appear when the player hovers over 
                            // the object 
    bool colliding = false; // Whether or not the object is currently colliding with a high magnitude impulse

    Rigidbody rb;

    // Called at the first frame, initialize all of the vertices
    void Start()
    { 
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        targetVertices = mesh.vertices;
        startingVertices = mesh.vertices;
        differences = new Vector3[vertices.Length];
        if (tag == "Transformable")
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Called once per frame
    // Consider changing to FixedUpdate()-- doesn't quite work but not sure why
    void Update()
    {
        // If object is currently colliding, revert the transformation
        if (colliding && counter <= transformTime) 
        {
            // Abruptly go back
            vertices = startingVertices;
            mesh.vertices = vertices;
            targetVertices = mesh.vertices;
            GetComponent<MeshCollider>().sharedMesh = mesh;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            counter = transformTime + 1; // set counter over transformTime to signal that the transformation is over
            if (transApplied.Count > 0) transApplied.RemoveAt(transApplied.Count - 1);
        }
        // Otherwise, if a transformation is in progress,
        // linearly move each vertex to the target vertex
        else if (counter <= transformTime)
        {
            maxDist = Vector3.zero;

            // Turn off gravity during the transformation
            if (tag == "Transformable")
            {
                rb.useGravity = false;
            }

            // Advance current vertices
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                vertices[i] += differences[i] * Time.deltaTime / transformTime;
                calcMaxes(i);
            }

            // Update the mesh
            mesh.vertices = vertices;
            GetComponent<MeshCollider>().sharedMesh = mesh;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            // Update the counter
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

    // Finds the largest position values (x, y, and z) in the vertices, to be used for
    // rendering the axes that appear when the player hovers over the object 
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

    /* This method applies a transformation to the gameobject. It restricts the number of 
    transformations that can be applied to the gameobject. */
    public void TransformShape(Vector3[] matrix, bool inverse, string name) {
        // Don't allow another transformation if currently transforming,
        // or if we have hit the max number of transformations allowed
        if (counter < transformTime || !TransformationAllowed(inverse, name))
        {
            return;
        }
        
        // Set starting vertices equal to current vertices, such that we can
        // revert back to starting position if the transformation doesn't work
        // (high impulse, e.g., object squeezed between two walls)
        for (int i = 0; i < vertices.Length; i++) {
            this.startingVertices[i] = new Vector3(vertices[i].x, vertices[i].y, vertices[i].z);
        }

        // Define where the vertices need to go
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
        
        // Reset the counter (starting a new transformation)
        counter = 0;
    }

    /*
    This method determines if the transformation we're trying to apply is allowed, i.e.,
    if we haven't hit the max number of transformations. If we have already applied transformation
    A, we consider applying A's inverse (A^-1) as subtracting from the total count. Similarly,
    if we have already applied transformation A^-1, we consider applying A as subtracting from the 
    total count. If we apply A or A^-1 before applying its inverse (A^-1 and A, respectively), then
    we add to the total count. We keep track of the total count by storing the transformations in a list.
    */
    bool TransformationAllowed(bool inverse, string name)
    {
        if (!inverse)
        {
            if (!transApplied.Remove(name + "I"))
            {
                if (transApplied.Count == maxApplied) return false;
                transApplied.Add(name);
            }
        }
        else
        {
            if (!transApplied.Remove(name))
            {
                if (transApplied.Count == maxApplied) return false;
                transApplied.Add(name + "I");
            }
        }

        return true;
    }

    // Built-in Unity support; triggers upon collision, keeps track if magnitude is big enough
    // to revert a transformation
    void OnCollisionStay(Collision info)
    {
        if (info.impulse.magnitude > 10)
        {
            colliding = true;
        } else 
        {
            colliding = false;
        }
    }

    // Built-in Unity support; once we stop colliding, reset the colliding variable to false
    void OnCollisionExit(Collision info)
    {
        colliding = false;
    }

}

