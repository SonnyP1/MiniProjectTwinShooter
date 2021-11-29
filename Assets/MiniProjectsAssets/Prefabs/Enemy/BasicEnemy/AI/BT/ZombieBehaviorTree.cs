using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviorTree : BehaviorTree
{
    public override void Init(AIController aiController)
    {
        base.Init(aiController);
        aiController.AddBlackBoardKey("Target");
        aiController.AddBlackBoardKey("LastKnownLoc");
        Sequence RootSequence = new Sequence(aiController);
        
        BTTask_MoveTo MoveToTarget = new BTTask_MoveTo(aiController, "Target",21f);
        BlackboardDecorator MoveToTargetDeco = new BlackboardDecorator(aiController, MoveToTarget, "Target", EKeyQuery.Set, EObserverAborts.Both);
        RootSequence.AddChild(MoveToTargetDeco);
        
        BTTask_AttackTarget AttackTarget = new BTTask_AttackTarget(aiController,"Target",21f);
        RootSequence.AddChild(AttackTarget);

       

        


        Sequence MoveThenCheck = new Sequence(aiController);
            BTTask_MoveTo MoveToLastKnowLoc = new BTTask_MoveTo(aiController, "LastKnownLoc", 0.5f);
            BlackboardDecorator MoveToLastKnowLocDeo = new BlackboardDecorator(aiController, MoveToLastKnowLoc, "LastKnownLoc", EKeyQuery.Set, EObserverAborts.Both);
            MoveThenCheck.AddChild(MoveToLastKnowLocDeo);
            BTTask_ClearBlackboardVal ClearLastKnowLocVal = new BTTask_ClearBlackboardVal(aiController, "LastKnownLoc");
            MoveThenCheck.AddChild(ClearLastKnowLocVal);
        RootSequence.AddChild(MoveThenCheck);

        SetRoot(RootSequence);
    }
}
