using System.Collections.Generic;
public abstract class Composite: BTNode
{
    private List<BTNode> _children = new List<BTNode>();

    public void AddChild(BTNode newChild)
    {
        if (!_children.Contains(newChild))
        {
            _children.Add(newChild);
            newChild.Parent = this;
        }
    }

    public bool RunNext()
    {
        _currentRunningChild.Finish();
        _currentRunningChildIndex++;
        if (_currentRunningChildIndex < _children.Count)
        {
            _currentRunningChild = _children[_currentRunningChildIndex];
            return true;
        }

        return false;
    }
    private BTNode _currentRunningChild;
    private int _currentRunningChildIndex;

    protected Composite(AIController aiController) : base(aiController)
    {
        
    }

    public override EBTTaskResult Execute()
    {
        if (_children.Count > 0)
        {
            _currentRunningChild = _children[0];
            _currentRunningChildIndex = 0;
            return EBTTaskResult.Running;
        }

        return EBTTaskResult.Success;
    }

    public override EBTTaskResult UpdateTask()
    {
        EBTTaskResult result = EBTTaskResult.Failure;
        if (!_currentRunningChild.HasStarted())
        {
            result = _currentRunningChild.Start();
            return DetermineResults(result);
        }
        result = _currentRunningChild.Update();
        return DetermineResults(result);
    }

    public abstract EBTTaskResult DetermineResults(EBTTaskResult result);

    public override void FinishTask()
    {
        foreach (var node in _children)
        {
            if (_children.Count > 0)
            {
                _currentRunningChild = _children[0];
                _currentRunningChildIndex = 0;
            }
            node.Finish();
        }
    }

    public int GetChildIndex(BTNode btNode)
    {
        return _children.FindIndex(0, target => { return btNode == target;});
    }
}