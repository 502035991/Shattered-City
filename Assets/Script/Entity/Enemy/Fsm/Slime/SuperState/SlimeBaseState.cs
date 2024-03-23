using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBaseState : EnemyState
{
    protected SlimeEnemy enemy;
    public SlimeBaseState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = (SlimeEnemy)enemy;
    }
}
