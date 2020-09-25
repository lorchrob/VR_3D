﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class MatrixInv : MonoBehaviour
{
    // Allows for a single instance of the Inventory to be accessed anywhere
    public static MatrixInv instance;

    void Awake()
    {
        // Checks to make sure there is always one instance of the inventory in the scene.
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory Found");
        }
        instance = this;
    }

    // This can be used to check whether the contents of the inventory changes
    public delegate void OnMatrixChanged();
    public OnMatrixChanged onMatrixChangedCallBack;

    public TransMatrix equippedMatrix;

    // Inventory itself
    public List<TransMatrix> matrices = new List<TransMatrix>();

    public int equippedIndex = 0; // Defines the index of the equipped Matrix

    public Text above;
    public Text below;
    public Text equipped;

    void Start()
    {
        for(int i = 0; i < matrices.Count; i++)
        {
            matrices[i].calcInverse();
        }
        UpdateText();
        EquipMatrix();
    }

    // Move up the list of matrices
    public void MoveUp()
    {
        if (equippedIndex < matrices.Count-1)
        {
            equippedIndex++;
            EquipMatrix();
        }
    }

    // Move down the list of matrices
    public void MoveDown()
    {
        if (equippedIndex > 0)
        {
            equippedIndex--;
            EquipMatrix();
        }
    }

    // Update the equipped matrix;
    void EquipMatrix()
    {
        equippedMatrix = matrices[equippedIndex];
        UpdateText();
    }

    // Update the inventory menu
    void UpdateText()
    {
        equipped.text = matrices[equippedIndex].name;

        if (equippedIndex == matrices.Count - 1)
        {
            below.text = "";
        }
        else
        {
            below.text = matrices[equippedIndex + 1].name;
        }

        if (equippedIndex == 0)
        {
            above.text = "";
        }
        else
        {
            above.text = matrices[equippedIndex - 1].name;
        }
    }


    // Add a new matrix to the inventory
    public bool Add(TransMatrix matrix)
    {
        matrix.calcInverse();
        matrices.Add(matrix);

        if (onMatrixChangedCallBack != null)
            onMatrixChangedCallBack.Invoke();

        return true;
    }

    // Remove a certain matrix from the inventory
    public void Remove(TransMatrix matrix)
    {
        matrices.Remove(matrix);

        if (onMatrixChangedCallBack != null)
            onMatrixChangedCallBack.Invoke();
    }


}
