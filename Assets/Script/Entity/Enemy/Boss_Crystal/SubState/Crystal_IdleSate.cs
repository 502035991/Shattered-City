using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_IdleSate : Crystal_GroundedState
{
    public Crystal_IdleSate(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();
        switch(enemy.currentPhase)
        {
            case Phase.One:
                enemyStateMachine.ChangeState(enemy.moveState);
                break;
            case Phase.Two: 
                break;
             case Phase.Three:
                break;       
        }
    }
    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(0);
    }
}
