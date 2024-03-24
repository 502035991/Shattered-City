using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    protected Enemy_Skeleton enemy;
    public SkeletonDeadState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = (Enemy_Skeleton)enemy;
    }

    public override void Enter()
    {
        base.Enter();
        isDead= true;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if (enemy.isControlled)
            enemy.SetVelocity(new Vector2(0, -10));
        else
            enemy.SetVelocity(Vector2.zero);
    }

    public override void Update()
    {
        base.Update();
    }
}
