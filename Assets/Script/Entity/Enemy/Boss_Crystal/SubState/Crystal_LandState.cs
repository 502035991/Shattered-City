using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_LandState : Crystal_AbilityState
{
    public Crystal_LandState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalCD> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
    {
    }

    public override  async void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        await UniTask.Delay(600);
        isAbilityDone = true;
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        enemy.SetVelocityX(0);
    }
}
