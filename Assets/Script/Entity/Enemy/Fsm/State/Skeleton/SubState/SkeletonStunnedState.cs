using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : SkeletonGroundedState
{
    public SkeletonStunnedState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if(!enemy.isControlled && enemy.CheckIfTouchingGround())
        {
            enemy.stateMachine.ChangeState(enemy.battleState , 0);
        }
    }
}
