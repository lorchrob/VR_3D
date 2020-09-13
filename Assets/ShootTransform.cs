using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTransform : MonoBehaviour
{
    public Transform firePoint;
    public TransMatrix equipedTransform;

    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Transformable") {
                    hit.collider.gameObject.GetComponent<BlenderMesh>().TransformShape(equipedTransform);
                }
                Debug.Log(hit.point);
                Debug.Log(hit.collider.tag);
            }
        }
    }




}
