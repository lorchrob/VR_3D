using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

// Script for displaying linear transformation matrix on the screen
// NOTE: This script is not currently in use; it was built for the old version of the project
public class MatrixDisplay : MonoBehaviour
{
    public Text matrix; // Assigned to text object in inspector
    MeshGenerator meshGenerator;

    // Use this for initialization
    void Start()
    {
        // Display multiple lines correctly
        matrix.horizontalOverflow = HorizontalWrapMode.Wrap;
        matrix.verticalOverflow = VerticalWrapMode.Overflow;

        GameObject mesh = GameObject.Find("Mesh");
        meshGenerator = mesh.GetComponent<MeshGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] transformMatrix = meshGenerator.transformMatrix;
        string text = "";

        // Display transformMatrix values from MeshGenerator script
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                text += string.Format("{0:00.0}", transformMatrix[i][j]);
                text += "  ";
            }
            text += "\n";
        }

        matrix.text = text;
    }
}
