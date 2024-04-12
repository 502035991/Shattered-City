using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_InAirState : EnemyState
{
    protected Boss_Crystal enemy;
    public Crystal_InAirState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = enemy;
    }

    private bool isGrounded;
    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = enemy.CheckIfTouchingGround();
    }

    public override void Update()
    {
        base.Update();
        if (isGrounded && enemy.currentVelocity.y < 0.01f)
        {
            enemyStateMachine.ChangeState(enemy.landState);
        }
        else
        {
            enemy.anim.SetFloat("yVelocity", enemy.currentVelocity.y);
        }
    }
}
