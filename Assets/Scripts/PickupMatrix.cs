using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is applied to a gameobject which, when a player walks over the
   gameobject, gives them a new linear transformation. The script animates the 
   gameobject to visually display the linear transformation it holds. */
public class PickupMatrix : MonoBehaviour
{
    public TransMatrix matrix; // The matrix the gameobject stores
    public float pauseTime; // Pause between transformations
    MatrixInv inventory; 
    float counter;
    public int state; // State = 0: Original Object, State = 1: Matrix Applied, State = -1: Inverse Applied
    bool invert;

    // Called once at the very beginning
    void Start()
    {
        inventory = MatrixInv.instance;
        counter = 0;
        invert = false;
        matrix.calcInverse();
        pauseTime = .1f;
        state = 0;
    }

    // Called once per frame
    // This method animates the gameobject by repeatedly applying the transformation
    // it holds and its inverse (in a certain pattern).
    void Update()
    {
        if(counter >= GetComponent<BlenderMesh>().transformTime + pauseTime && !invert)
        {
            GetComponent<BlenderMesh>().TransformShape(matrix.matrix, false, "animate");
            state++;
            counter = 0;
            if (state == 1)
            {
                invert = true;
            }
        }
        
        else if(counter >= GetComponent<BlenderMesh>().transformTime + pauseTime && invert)
        {
            GetComponent<BlenderMesh>().TransformShape(matrix.invertMatrix, true, "animate");
            state--;
            counter = 0;
            if (state == -1)
            {
                invert = false;
            }
        }
        

        counter += Time.deltaTime;
    }

    // If the player hits the gameobject's collider, they pick up the transformation it stores
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inventory.Add(matrix);
            Destroy(gameObject);
        }
    }
}
