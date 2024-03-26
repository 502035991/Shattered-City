using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_SkillState1 : Crystal_AbilityState
{
    public Crystal_SkillState1(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName, Action<CrystalCD> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemyStateMachine.ChangeState(enemy.idleState, 2f);
    }

    public override void Exit()
    {
        base.Exit();
        isAbilityDone = true;
    }
}
