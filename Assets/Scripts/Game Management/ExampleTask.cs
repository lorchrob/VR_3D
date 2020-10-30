using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTask : GameTask
{
    public string completionText;

    public override bool IsCompleted()
    {
        return transform.position.y < 0;
    }

    public override void OnCompleted()
    {
        Debug.Log(completionText);
    }
}
