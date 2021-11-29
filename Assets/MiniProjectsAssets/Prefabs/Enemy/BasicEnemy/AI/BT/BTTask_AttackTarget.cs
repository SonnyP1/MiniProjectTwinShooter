using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AttackTarget();
public class BTTask_AttackTarget : BTNode
{
    private float _acceptableRadius;
    private string _keyName;
    public BTTask_AttackTarget(AIController aiController,string keyName,float acceptableRadius) : base(aiController)
    {
        _acceptableRadius = acceptableRadius;
        _keyName = keyName;
    }

    public override EBTTaskResult Execute()
    {
        if (AIC.GetBlackBoardVal("Target") != null && GetDestination(out Vector3 destination))
        {
            if (Vector3.Distance(AIC.transform.position, destination) <= _acceptableRadius)
            {
                Debug.Log($"Attack {AIC.GetBlackBoardVal("Target")}");
                AIC.onAttack.Invoke();
                return EBTTaskResult.Success;
            }
            return EBTTaskResult.Failure;
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
    
    
    bool GetDestination(out Vector3 Destination)
    {
        Destination = Vector3.negativeInfinity;
        object value = AIC.GetBlackBoardVal(_keyName);
        if (value == null)
        {
            return false;
        }
        if (value.GetType() == typeof(GameObject))
        {
            Destination = ((GameObject) value).transform.position;
            return true;
        }

        if (value.GetType() == typeof(Vector3))
        {
            Destination = (Vector3) value;
            return true;
        }

        return false;
    }
}
