using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform destination;

    bool holding = false;

    void Update()
    {
        if (holding)
        {
            this.transform.position = destination.position;
        }
    }

    void OnMouseDown() 
    {
        // Right now, I can't even get the onMouseDown to trigger the 
        // "Got here" message. If we can get onMouseDown() and onMouseUp()
        // to trigger (or switch to a different way to trigger pick up and drop), then
        // I am pretty confident in the code
        print("Got here");
        GetComponent<Rigidbody>().useGravity = false;
        //GetComponent<BoxCollider>().enabled = false;
        holding = true;
        //this.transform.position = destination.position;
        //this.transform.parent = GameObject.Find("Firepoint").transform;
    }

    void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        //GetComponent<BoxCollider>().enabled = true;
        holding = false;
    }
}
