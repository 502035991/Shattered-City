using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBaseState : EnemyState
{
    protected Enemy_Slime enemy;
    public SlimeBaseState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = (Enemy_Slime)enemy;
    }
}
