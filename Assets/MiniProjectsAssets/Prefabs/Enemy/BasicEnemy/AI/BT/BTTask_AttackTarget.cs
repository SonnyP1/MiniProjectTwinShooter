using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public delegate void AttackTarget();
public class BTTask_AttackTarget : BTNode
{
    private float _acceptableRadius;
    private string _keyName;
    private NavMeshAgent _agent;
    public BTTask_AttackTarget(AIController aiController,string keyName) : base(aiController)
    {
        _keyName = keyName;
        _agent = aiController.GetComponent<NavMeshAgent>();

    }

    public override EBTTaskResult Execute()
    {
        _acceptableRadius = (float)AIC.GetBlackBoardVal("AttackRange");
        if (AIC.GetBlackBoardVal("Target") != null && GetDestination(out Vector3 destination))
        {
            if (Vector3.Distance(AIC.transform.position, destination) <= _acceptableRadius)
            {
                Debug.Log($"Attack {AIC.GetBlackBoardVal("Target")}");
                AIC.onAttack.Invoke();
                return EBTTaskResult.Running;
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
        GameObject targetToLookAt = (GameObject) AIC.GetBlackBoardVal("Target");
        if (targetToLookAt != null)
        {
            Vector3 aiDir = _agent.transform.forward;
            Vector3 targetDir = (targetToLookAt.transform.position - AIC.transform.position).normalized;
            
            Quaternion goalRotation = Quaternion.LookRotation(targetDir, Vector3.up);
            _agent.transform.rotation = Quaternion.Lerp(_agent.transform.rotation,goalRotation,Time.deltaTime*9f);


            if (Vector3.Dot(aiDir, targetDir) >= .99)
            {
                return EBTTaskResult.Success;
            }
            
            return EBTTaskResult.Running;
        }

        return EBTTaskResult.Failure;
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
