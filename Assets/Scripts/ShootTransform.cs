using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootTransform : MonoBehaviour
{
    public Transform firePoint;

    MatrixInv inventory;

    void Start()
    {
        inventory = MatrixInv.instance; // Pointer to the global inventory instance
    }

    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Transformable") {
                    hit.collider.gameObject.GetComponent<BlenderMesh>().TransformShape(inventory.equippedMatrix.matrix);
                }
                Debug.Log(hit.point);
                Debug.Log(hit.collider.tag);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Transformable")
                {
                    hit.collider.gameObject.GetComponent<BlenderMesh>().TransformShape(inventory.equippedMatrix.invertMatrix);
                }
                Debug.Log(hit.point);
                Debug.Log(hit.collider.tag);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            inventory.MoveDown();
        }
    }




}
