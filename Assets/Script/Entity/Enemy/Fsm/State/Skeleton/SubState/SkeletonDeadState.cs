using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    protected SkeletonEnemy enemy;
    public SkeletonDeadState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = (SkeletonEnemy)enemy;
    }

    public override void Enter()
    {
        base.Enter();
        isDead= true;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        enemy.SetVelocity(Vector2.zero);
    }

    public override void Update()
    {
        base.Update();
    }
}
