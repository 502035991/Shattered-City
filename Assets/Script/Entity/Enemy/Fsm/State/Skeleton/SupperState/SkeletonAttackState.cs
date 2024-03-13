using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    protected SkeletonEnemy enemy;

    public SkeletonAttackState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = (SkeletonEnemy)EnemyBase;
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.SetVelocityX(0);
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("");
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if(!enemy.isHurt)
            enemy.SetVelocityX(0);
    }
    public override void AnimationFinishTrigger()
    {
        enemyStateMachine.ChangeState(enemy.battleState);
    }
    public override void SetAdditionalData(object value)
    {
        if((bool)value)
            enemy.anim.SetTrigger("IsCritical");
    }
}
