using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_JumpState : Crystal_AbilityState
{
    public Crystal_JumpState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName, Action<CrystalCD> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
    {
    }
}
