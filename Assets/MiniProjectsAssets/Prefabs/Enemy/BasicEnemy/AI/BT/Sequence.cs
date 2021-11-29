public class Sequence : Composite
{
    public Sequence(AIController aiController) : base(aiController)
    {
        
    }

    public override EBTTaskResult DetermineResults(EBTTaskResult result)
    {
        
        if (result == EBTTaskResult.Failure)
        {
            return EBTTaskResult.Failure;
        }
        if(result == EBTTaskResult.Success)
        {
            if (RunNext())
            {
                return EBTTaskResult.Running;
            }
            else
            {
                return EBTTaskResult.Success;
            }
        }

        return EBTTaskResult.Running;
    }
}