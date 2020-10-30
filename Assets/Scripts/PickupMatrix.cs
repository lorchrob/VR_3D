using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMatrix : MonoBehaviour
{

    public TransMatrix matrix;
    public float pauseTime; // Pause between transformations

    MatrixInv inventory;

    float counter;

    public int state; // State = 0: Original Object, State = 1: Matrix Applied, State = -1: Inverse Applied

    bool invert;

    void Start()
    {
        inventory = MatrixInv.instance;
        counter = 0;
        invert = false;
        matrix.calcInverse();

        state = 0;
    }

    void Update()
    {
        if(counter >= GetComponent<BlenderMesh>().transformTime + pauseTime && !invert)
        {
            GetComponent<BlenderMesh>().TransformShape(matrix.matrix);
            state++;
            counter = 0;
            if (state == 1)
            {
                invert = true;
            }
        }
        
        else if(counter >= GetComponent<BlenderMesh>().transformTime + pauseTime && invert)
        {
            GetComponent<BlenderMesh>().TransformShape(matrix.invertMatrix);
            state--;
            counter = 0;
            if (state == -1)
            {
                invert = false;
            }
        }
        

        counter += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inventory.Add(matrix);
            Destroy(gameObject);
        }
    }
}
