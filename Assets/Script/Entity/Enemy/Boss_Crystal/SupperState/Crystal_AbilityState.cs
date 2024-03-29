using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_AbilityState : EnemyState
{
    protected Boss_Crystal enemy;
    protected Action<CrystalCD> ac;

    protected bool isAbilityDone;
    protected bool isGrounded;
    public Crystal_AbilityState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalCD> ac) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = enemy;
        this.ac = ac;
    }
    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }
    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = enemy.CheckIfTouchingGround();
        if (isAbilityDone)
        {
            if (isGrounded && enemy.currentVelocity.y < 0.01f)
            {
                enemyStateMachine.ChangeState(enemy.idleState);
            }
            else
            {
                enemyStateMachine.ChangeState(enemy.inAirState);
            }
        }

    }
}
