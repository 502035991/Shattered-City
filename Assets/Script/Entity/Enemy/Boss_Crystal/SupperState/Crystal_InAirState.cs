using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_InAirState : EnemyState
{
    protected Boss_Crystal enemy;
    public Crystal_InAirState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = (Boss_Crystal)enemy;
    }
}
