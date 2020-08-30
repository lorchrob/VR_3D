using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

// Script for displaying linear transformation matrix on the screen
public class MatrixDisplay : MonoBehaviour
{
    public Text matrix;
    MeshGenerator meshGenerator;

    // Use this for initialization
    void Start()
    {
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
                text += Math.Round(transformMatrix[i][j], 0);
                text += " ";
            }
            text += "|";
            //text += "\n";
        }

        matrix.text = text;
    }
}
