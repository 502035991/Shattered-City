using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_AttackState : Crystal_AbilityState
{
    public Crystal_AttackState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    public override void DoCheck()
    {
        base.DoCheck();

    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(0);
    }


}
