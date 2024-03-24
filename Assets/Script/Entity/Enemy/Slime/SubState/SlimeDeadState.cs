using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeadState : SlimeBaseState
{
    public SlimeDeadState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isDead = true;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if (enemy.isControlled)
            enemy.SetVelocity(new Vector2(0, -10));
        else
            enemy.SetVelocity(Vector2.zero);
    }


}
