using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_LandState : Crystal_GroundedState
{
    public Crystal_LandState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }
}
