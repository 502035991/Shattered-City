using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_DeadState : Crystal_AbilityState
{
    public Crystal_DeadState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalCD> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
    {

    }
}
