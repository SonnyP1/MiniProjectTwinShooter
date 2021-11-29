using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    private AIController _aiController;
    public BTNode CurrentRunningNode { get; set; }
    private BTNode _root;

    public void SetRoot(BTNode newRoot)
    {
        _root = newRoot;
    }
    public virtual void Init(AIController aiController)
    {
        _aiController = aiController;
    }

    public void Run()
    {
        BTNode.EBTTaskResult results = BTNode.EBTTaskResult.Failure;
        if (!_root.HasStarted())
        {
            results = _root.Start();
            if (results != BTNode.EBTTaskResult.Running)
            {
                _root.Finish();
                return;
            }
        }

        results = _root.Update();
        if (results != BTNode.EBTTaskResult.Running)
        {
            _root.Finish();
        }
    }

    public void Restart()
    {
        _root.Finish();
    }

    public bool IsRunning(BTNode btNode)
    {
        if (GetHiarchy(CurrentRunningNode).Contains(btNode))
        {
            return true;
        }

        return false;
    }

    List<BTNode> GetHiarchy(BTNode btNode)
    {
        List<BTNode> Hierachy = new List<BTNode>();
        BTNode NextInHierachy = btNode;
        Hierachy.Add(NextInHierachy);
        while (NextInHierachy.Parent != null)
        {
            Hierachy.Add(NextInHierachy.Parent);
            NextInHierachy = NextInHierachy.Parent;
        }
        Hierachy.Reverse();
        return Hierachy;
    }

    public bool IsCurrentLowerThan(BTNode btNode)
    {
        List<BTNode> CurrentRunningHierachy = GetHiarchy(CurrentRunningNode);
        List<BTNode> BTNodeHierachy = GetHiarchy(btNode);
        for (int i = 0; i < CurrentRunningHierachy.Count && i < BTNodeHierachy.Count; i++)
        {
            BTNode CurrentParent = CurrentRunningHierachy[i];
            BTNode NodeParent = BTNodeHierachy[i];
            if (CurrentParent == _root || NodeParent == _root)
            {
                continue;
            }

            if (CurrentParent != NodeParent)
            {
                return CurrentParent.GetNodeIndexInParent() > NodeParent.GetNodeIndexInParent();
            }
        }

        return false;
    }
}
