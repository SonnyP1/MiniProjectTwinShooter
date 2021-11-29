public class BTTask_AlwaysFails : BTNode
{
    public BTTask_AlwaysFails(AIController aiController) : base(aiController)
    {
    }

    public override EBTTaskResult Execute()
    {
        return EBTTaskResult.Failure;
    }

    public override EBTTaskResult UpdateTask()
    {
        return EBTTaskResult.Failure;
    }

    public override void FinishTask()
    {
    }
}