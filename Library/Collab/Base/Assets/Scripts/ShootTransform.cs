using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This script gives the player the capability to fire the linear transformation
cannon, as well as cycle through transformations in the inventory. */
public class ShootTransform : MonoBehaviour
{
    public Transform firePoint;

    MatrixInv inventory;

    void Start()
    {
        inventory = MatrixInv.instance; // Pointer to the global inventory instance
    }

    // Called once per frame.
    // When a key is pressed, perform the corresponding action.
    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (RaycastTransformable(out hit)) {
                hit.collider.gameObject.GetComponent<BlenderMesh>().TransformShapeRestricted(inventory.equippedMatrix.matrix,
                                                                                             false,
                                                                                             inventory.equippedMatrix.name);
            }
            //Debug.Log(hit.point);
            //Debug.Log(hit.collider.tag);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (RaycastTransformable(out hit))
            {
                hit.collider.gameObject.GetComponent<BlenderMesh>().TransformShapeRestricted(inventory.equippedMatrix.invertMatrix,
                                                                                             true,
                                                                                             inventory.equippedMatrix.name);
            }
            //Debug.Log(hit.point);
            //Debug.Log(hit.collider.tag);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            inventory.MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // Should make sure the player wants to continue
            SceneManager.LoadScene (SceneManager.GetActiveScene().name);
        }
    }

    /* Performs a raycast out from the fire point into the game world.
     * 
     * If the raycast hits a collider, returns whether or not the collider is
     * tagged as transformable. If the raycast hits no colliders, return false.
     */
    public bool RaycastTransformable(out RaycastHit hit)
    {
        if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            return hit.collider.tag == "Transformable";
        else
            return false;
    }
}
