using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_GroundedState : EnemyState
{
    protected Boss_Crystal enemy;

    public Crystal_GroundedState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = enemy;
    }
}
