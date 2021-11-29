using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_ClearBlackboardVal : BTNode
{
    private string _key;
    public BTTask_ClearBlackboardVal(AIController aiController,string key) : base(aiController)
    {
        _key = key;
    }

    public override EBTTaskResult Execute()
    {
        AIC.SetBlackBoardKey(_key,null);
        Debug.Log($"SetBlackboard key {_key} to null");
        if (AIC.GetBlackBoardVal(_key) == null)
        {
            return EBTTaskResult.Success;
        }
        else
        {
            return EBTTaskResult.Failure;
        }
    }

    public override EBTTaskResult UpdateTask()
    {
        return EBTTaskResult.Running;
    }

    public override void FinishTask()
    {
        
    }
}
