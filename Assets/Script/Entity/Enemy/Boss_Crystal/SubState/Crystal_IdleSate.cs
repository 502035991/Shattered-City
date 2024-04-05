using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_IdleSate : Crystal_GroundedState
{

    private bool isStop = false;

    public Crystal_IdleSate(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if(!isStop)
        {
            switch (enemy.currentPhase)
            {
                case Phase.One:
                    enemyStateMachine.ChangeState(enemy.moveState);
                    break;
                case Phase.Two:
                    enemyStateMachine.ChangeState(enemy.jumpState);
                    break;
                case Phase.Three:
                    break;
            }
        }
    }
    public override async void SetAdditionalData(object value)
    {
        base.SetAdditionalData(value);
        if (value is float)
        {
            isStop = true;
            await UniTask.Delay(TimeSpan.FromSeconds((float)value));
            isStop = false;
        }
    }
    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(0);
    }

}
