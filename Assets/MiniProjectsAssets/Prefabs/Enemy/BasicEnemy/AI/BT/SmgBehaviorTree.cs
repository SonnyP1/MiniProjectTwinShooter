using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmgBehaviorTree : BehaviorTree
{
    public override void Init(AIController aiController)
    {
        base.Init(aiController);
        aiController.AddBlackBoardKey("Target");
        aiController.AddBlackBoardKey("LastKnownLoc");
        aiController.AddBlackBoardKey("AttackRange");
        Selector RootSelector = new Selector(aiController);
        
        BTTask_AttackTarget AttackTarget = new BTTask_AttackTarget(aiController,"Target");
        RootSelector.AddChild(AttackTarget);

        BTTask_MoveTo MoveToTarget = new BTTask_MoveTo(aiController, "Target",8f);
        BlackboardDecorator MoveToTargetDeco = new BlackboardDecorator(aiController, MoveToTarget, "Target", EKeyQuery.Set, EObserverAborts.Both);
        RootSelector.AddChild(MoveToTargetDeco);

        Sequence MoveThenCheck = new Sequence(aiController);
            BTTask_MoveTo MoveToLastKnowLoc = new BTTask_MoveTo(aiController, "LastKnownLoc", 0.5f);
            BlackboardDecorator MoveToLastKnowLocDeo = new BlackboardDecorator(aiController, MoveToLastKnowLoc, "LastKnownLoc", EKeyQuery.Set, EObserverAborts.Both);
            MoveThenCheck.AddChild(MoveToLastKnowLocDeo);
            BTTask_ClearBlackboardVal ClearLastKnowLocVal = new BTTask_ClearBlackboardVal(aiController, "LastKnownLoc");
            MoveThenCheck.AddChild(ClearLastKnowLocVal);
        RootSelector.AddChild(MoveThenCheck);

        SetRoot(RootSelector);
    }
}
