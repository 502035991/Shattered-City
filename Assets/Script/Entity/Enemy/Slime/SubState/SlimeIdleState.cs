using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : SlimeBaseState
{
    public SlimeIdleState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    private bool isTouchingGround;
    public override void DoCheck()
    {
        base.DoCheck();
        isTouchingGround = enemy.CheckIfTouchingGround();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(0);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if(isTouchingGround)
            enemyStateMachine.ChangeState(enemy.moveState);
    }
}
