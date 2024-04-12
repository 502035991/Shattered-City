using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_ChangePhaseState : Crystal_GroundedState
{

    private float duration = 5f;
    private float startTime;

    public Crystal_ChangePhaseState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetPhase(Phase.Two);
        enemy.SetHurtState(false);
        enemy.stats.ResetCurrentHealth();

        startTime = Time.time;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if(Time.time > startTime +duration)
        {
            enemyStateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit() 
    { 
        base.Exit();
        enemy.SetHurtState(true); 
    }
}
