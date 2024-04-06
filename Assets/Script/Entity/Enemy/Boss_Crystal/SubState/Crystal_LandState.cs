using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_LandState : Crystal_GroundedState
{
    public Crystal_LandState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }
    public override void DoCheck()
    {
        //≤ª÷¥––∏∏¿‡
    }
    public override  async void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        await UniTask.Delay(600);
        enemyStateMachine.ChangeState(enemy.idleState);
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        enemy.SetVelocityX(0);
    }
}
