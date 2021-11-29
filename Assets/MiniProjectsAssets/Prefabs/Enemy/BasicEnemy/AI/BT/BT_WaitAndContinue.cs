using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_WaitAndContinue : BTNode
{

    private float _waitTime;
    private float _counter;
    public BT_WaitAndContinue(AIController aiController, float waitTime) : base(aiController)
    {
        _waitTime = waitTime;
    }
    public override EBTTaskResult Execute()
    {
        Debug.Log($"Wait for {_waitTime}");
        _counter = 0;
        return EBTTaskResult.Running;
    }

    public override EBTTaskResult UpdateTask()
    {
        _counter += Time.deltaTime;
        if (_counter >= _waitTime)
        {
            return EBTTaskResult.Failure;
        }

        return EBTTaskResult.Running;
    }

    public override void FinishTask()
    {
        Debug.Log("Wait DONE");
    }
}
