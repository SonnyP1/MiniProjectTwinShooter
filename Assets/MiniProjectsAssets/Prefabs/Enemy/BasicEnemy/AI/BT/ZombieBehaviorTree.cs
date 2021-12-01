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
        aiController.AddBlackBoardKey("AttackRange");
        Selector RootSelector = new Selector(aiController);

        BTTask_MoveAndAttack MoveAndAttack = new BTTask_MoveAndAttack(aiController, "Target", 1.5f);
        BlackboardDecorator MoveAndAttackDeco = new BlackboardDecorator(aiController, MoveAndAttack, "Target", EKeyQuery.Set, EObserverAborts.Both);
        RootSelector.AddChild(MoveAndAttackDeco);
        

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
