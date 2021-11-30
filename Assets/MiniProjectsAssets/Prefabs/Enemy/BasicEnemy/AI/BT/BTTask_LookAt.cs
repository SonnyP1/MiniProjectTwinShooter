using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_LookAt : BTNode
{
    private string _keyName;
    private NavMeshAgent _agent;
    public BTTask_LookAt(AIController aiController,string keyName) : base(aiController)
    {
        _keyName = keyName;
        _agent = AIC.GetComponent<NavMeshAgent>();
    }

    public override EBTTaskResult Execute()
    {
        return EBTTaskResult.Running;
    }

    public override EBTTaskResult UpdateTask()
    {
        GameObject targetToLookAt = (GameObject) AIC.GetBlackBoardVal("Target");
        if (targetToLookAt != null)
        {
            Vector3 aiDir = _agent.transform.forward;
            Vector3 targetDir = (targetToLookAt.transform.position - AIC.transform.position).normalized;
            
            Quaternion goalRotation = Quaternion.LookRotation(targetDir, Vector3.up);
            _agent.transform.rotation = Quaternion.Lerp(_agent.transform.rotation,goalRotation,Time.deltaTime*8f);


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
}
