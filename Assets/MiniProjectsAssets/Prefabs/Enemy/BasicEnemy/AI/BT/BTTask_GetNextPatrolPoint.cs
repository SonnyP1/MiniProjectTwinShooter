
    using UnityEngine;

    public class BTTask_GetNextPatrolPoint:BTNode
    {
        public BTTask_GetNextPatrolPoint(AIController aiController) : base(aiController)
        {

        }

        public override EBTTaskResult Execute()
        {
            GameObject nextPatrolPoint = AIC.GetComponent<PatrollingComponent>().GetNextPatrolPoint();
            if (nextPatrolPoint != null)
            {
                AIC.SetBlackBoardKey("patrolPoint",nextPatrolPoint);
                return EBTTaskResult.Success;
            }

            return EBTTaskResult.Failure;
        }

        public override EBTTaskResult UpdateTask()
        {
            return EBTTaskResult.Running;
        }

        public override void FinishTask()
        {
            
        }
    }