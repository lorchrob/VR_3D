using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLine : MonoBehaviour
{

    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) // Checks to see if the raycast actually hits something
        {
            Vector3[] points = { transform.position, hit.point };

            line.SetPositions(points);
        }
        else
        {
            Vector3[] points = { transform.position, transform.position + transform.forward * 100 };

            line.SetPositions(points);
        }

        
    }
}
