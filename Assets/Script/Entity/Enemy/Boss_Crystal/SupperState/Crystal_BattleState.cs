using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_BattleState : EnemyState
{
    public Crystal_BattleState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }
}
