public abstract class BTNode
{
    private bool ShouldAbortTask;

    public int GetNodeIndexInParent()
    {
        if (Parent.GetType() == typeof(Selector) || Parent.GetType()==typeof(Sequence))
        {
            return ((Composite) Parent).GetChildIndex(this);
        }

        return 0;
    }
    public BTNode Parent { set; get; }

    void AbortTask()
    {
        ShouldAbortTask = true;
    }
    public enum EBTTaskResult
    {
        Success,
        Failure,
        Running
    }
    public AIController AIC
    {
        get { return _aiController; }
    }
    
    private AIController _aiController;

    public BTNode(AIController aiController)
    {
        _aiController = aiController;
    }

    public bool HasStarted()
    {
        return _started;
    }

    private bool _started;
    private bool _finish;

    public EBTTaskResult Update()
    {
        if (!ShouldAbortTask)
        {
            return UpdateTask();
        }

        return EBTTaskResult.Failure;
    }
    public EBTTaskResult Start()
    {
        if (!_started)
        {
            ShouldAbortTask = false;
            _started = true;
            _finish = false;
            AIC.GetBehaviorTree().CurrentRunningNode = this;
            return Execute();
        }
        return EBTTaskResult.Running;
    }

    public virtual void Finish()
    {
        if (!_finish && HasStarted())
        {
            ShouldAbortTask = false;
            _finish = true;
            _started = false;
            FinishTask();
        }
    }

    public abstract EBTTaskResult Execute();
    public abstract EBTTaskResult UpdateTask();
    public abstract void FinishTask();
}