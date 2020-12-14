using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public GameObject tempParent; // Where the object goes when we pick it up (HoldPoint)
    Vector3 objectPos; // Stores this object's current position
    float distance; // Stores the distance between the HoldPoint and the gameobject
    bool isHolding = false; // Whether or not the item is currently being held

    // Update is called once per frame
    void Update () 
    {
        // Update distance
        distance = Vector3.Distance (transform.position, tempParent.transform.position);
    
        // Can't hold if too far away
        if (distance >= 1f) 
        {
            isHolding = false;
        }

        // If we're holding, set the velocities equal to zero and update the parent item
        if (isHolding == true) 
        {
            GetComponent<Rigidbody> ().velocity = Vector3.zero;
            GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
            transform.SetParent (tempParent.transform);
        }
        // Otherwise, reset the parent item and store the item's position
        else 
        {
            objectPos = transform.position;
            transform.SetParent (null);
            GetComponent<Rigidbody>().useGravity = true;
            transform.position = objectPos;
        }
    }

    // Triggers when the mouse hovers over the gameitem
    void OnMouseOver()
    {
        // If middle mouse button is pressed, try to pick up item
        if (Input.GetMouseButtonDown(2)) 
        {
            // Must be close enough to pick up
            if (distance <= 1f)
            {
                isHolding = true;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().detectCollisions = true;
            }
        }

        // If middle mouse button is released, let go of the item
        if (Input.GetMouseButtonUp(2)) 
        {
            isHolding = false;
        }
    }
}