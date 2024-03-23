using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected SkeletonEnemy enemy;
    public SkeletonGroundedState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = (SkeletonEnemy)enemy;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}
