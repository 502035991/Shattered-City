using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : SlimeBaseState
{

    private bool isTouchingGround;
    private bool isTouchingWall;

    public SlimeMoveState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();
        isTouchingGround = enemy.CheckIfTouchingGround();
        isTouchingWall = enemy.CheckIfTouchingWall();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.SetVelocityX(0);
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
    }
    public override void Update()
    {
        base.Update();
        if (!isTouchingGround || isTouchingWall)
        {
            enemy.SetFilp(-enemy.facingDirection);
        }
        else if(enemy.CheckIfPlayerToLine(out _))
        {
            enemyStateMachine.ChangeState(enemy.battleState);
        }
    }
    }
