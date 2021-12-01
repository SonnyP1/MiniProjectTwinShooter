using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveAndAttack : BTNode
{
    private string _keyName;
    private NavMeshAgent _agent;
    private float _acceptableMoveRadius;
    private float _acceptableAttackRadius;
    
    public BTTask_MoveAndAttack(AIController aiController,string keyName,float acceptableMoveRadius) : base(aiController)
    {
        _keyName = keyName;
        _agent = aiController.GetComponent<NavMeshAgent>();
        _acceptableMoveRadius = acceptableMoveRadius;
    }

    public override EBTTaskResult Execute()
    {
        if (_agent)
        {
            if (GetDestination(out Vector3 destination))
            {
                _agent.SetDestination(destination);
                _agent.isStopped = false;
                return EBTTaskResult.Running;
            }
        }

        return EBTTaskResult.Failure;
    }

    public override EBTTaskResult UpdateTask()
    {
        if (GetDestination(out Vector3 destination))
        {
            _agent.isStopped = false;
            _agent.SetDestination(destination);
            if (AIC.GetBlackBoardVal("Target") != null)
            {
                _acceptableAttackRadius = (float)AIC.GetBlackBoardVal("AttackRange");
                if (Vector3.Distance(AIC.transform.position, destination) <= _acceptableAttackRadius)
                {
                    Debug.Log($"Attack {AIC.GetBlackBoardVal("Target")}");
                    AIC.onAttack.Invoke();
                    return EBTTaskResult.Running;
                }
                return EBTTaskResult.Failure;
            }
            
            if (Vector3.Distance(AIC.transform.position, destination) <= _acceptableMoveRadius)
            {
                return EBTTaskResult.Success;
            }
            
        }
        else
        {
            return EBTTaskResult.Failure;
        }


        return EBTTaskResult.Running;
    }

    public override void FinishTask()
    {
        throw new System.NotImplementedException();
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
