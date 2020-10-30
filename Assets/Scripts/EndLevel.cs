using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : GameTask
{

    string completeText = "Level Complete!";
    bool isCompleted = false;

    public override bool IsCompleted()
    {
        return isCompleted;
    }

    public override void OnCompleted()
    {
        Debug.Log(completeText);
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            isCompleted = true;
        }
    }
}
