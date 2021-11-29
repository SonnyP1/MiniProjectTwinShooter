using UnityEngine;

public enum EKeyQuery
{
    Set,
    NotSet
}
public enum EObserverAborts
{
    None,
    Self,
    LowerPriority,
    Both
}
public class BlackboardDecorator :Decorator
{
    private string _keyName;
    private EKeyQuery _keyQuery;
    private EObserverAborts _observerAborts;
    public BlackboardDecorator(AIController aiController, BTNode child,string keyName,EKeyQuery keyQuery,EObserverAborts eObserverAborts) : base(aiController, child)
    {
        _keyName = keyName;
        _keyQuery = keyQuery;
        _observerAborts = eObserverAborts;
        AIC.onBlackBoardKeyUpdated += KeyUpdated;
    }

    public override EBTTaskResult Execute()
    {
        object val = AIC.GetBlackBoardVal(_keyName);

        if (ShouldDoTask(val))
        {
            return EBTTaskResult.Running;
        }

        return EBTTaskResult.Failure;
    }

    private void KeyUpdated(string key, object value)
    {
        Debug.Log($"{key} has change to: {value} ");
        if (key != _keyName)
        {
            return;
        }
        if (AIC.GetBehaviorTree().IsRunning(this))
        {
            if (!ShouldDoTask(value))
            {
                if (_observerAborts == EObserverAborts.Self || _observerAborts == EObserverAborts.Both)
                {
                    Finish();
                }
            }
        }
        else if (AIC.GetBehaviorTree().IsCurrentLowerThan(this))
        {
            if (ShouldDoTask(this))
            {
                if (_observerAborts == EObserverAborts.Both || _observerAborts == EObserverAborts.LowerPriority)
                {
                    AIC.GetBehaviorTree().Restart();
                }
            }
        }

    }

    public override EBTTaskResult UpdateTask()
    {
        if (GetChild().HasStarted())
        {
            return GetChild().Start();
        }

        return GetChild().Update();
    }

    public override void FinishTask()
    {
    }

    bool ShouldDoTask(object value)
    {
        switch (_keyQuery)
        {
            case EKeyQuery.Set:
                return value != null;
            case EKeyQuery.NotSet:
                return value == null;
            default:
                return false;
        }
        
    }
}
