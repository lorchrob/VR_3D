using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMenu : MonoBehaviour
{

    public Transform anchor;


    // Update is called once per frame
    void Update()
    {
        transform.position = anchor.position;
        transform.rotation = anchor.rotation;
    }
}
