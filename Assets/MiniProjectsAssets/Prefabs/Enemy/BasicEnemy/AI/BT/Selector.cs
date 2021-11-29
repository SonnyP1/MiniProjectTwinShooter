public class Selector: Composite
{
    public Selector(AIController aiController) : base(aiController)
    {
        
    }

    public override EBTTaskResult DetermineResults(EBTTaskResult result)
    {
        if (result == EBTTaskResult.Success)
        {
            return EBTTaskResult.Success;
        }
        if(result == EBTTaskResult.Failure)
        {
            if (RunNext())
            {
                return EBTTaskResult.Running;
            }
            else
            {
                return EBTTaskResult.Failure;
            }
        }

        return EBTTaskResult.Running;
    }
}