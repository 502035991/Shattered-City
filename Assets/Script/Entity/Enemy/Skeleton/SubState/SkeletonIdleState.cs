using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundedState
{
    private float timer;
    public SkeletonIdleState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(0);
        timer = 0.4f;
    }

    public override void Update()
    {
        base.Update();
        if(timer <0)
        {
            enemyStateMachine.ChangeState(enemy.moveState);
        }
    }
    public override void DoCheck()
    {
        base.DoCheck();
        timer -= Time.deltaTime;
    }
    public override void Exit()
    {
        base.Exit();
    }
}
