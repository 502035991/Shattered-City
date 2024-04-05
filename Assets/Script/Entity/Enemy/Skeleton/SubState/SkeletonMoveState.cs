using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    private bool isTouchingGround;
    private bool isTouchingWall;

    private CancellationTokenSource cancellationTokenSource;
    public SkeletonMoveState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void DoCheck()
    {
        base.DoCheck();
        isTouchingGround = enemy.CheckIfTouchingGround();
        isTouchingWall = enemy.CheckIfTouchingWall();
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
    }

    public override void Update()
    {
        base.Update();
        if(!isTouchingGround || isTouchingWall)
        {            
            enemyStateMachine.ChangeState(enemy.idleState);
            _ = WaitForSecond();
        }

        if (enemy.CheckIfPlayerToLine(out _))
        {
            enemyStateMachine.ChangeState(enemy.battleState);
        }
    }
    private async UniTaskVoid WaitForSecond()
    {
        cancellationTokenSource = new CancellationTokenSource();
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: cancellationTokenSource.Token);
            enemy.SetFlip(-enemy.facingDirection);
        }
        catch (OperationCanceledException)
        {
            // 如果操作被取消，什么都不做
        }
    }
    public override void Exit()
    {
        base.Exit();
        cancellationTokenSource?.Cancel();
        enemy.SetVelocityX(0);
    }

}
