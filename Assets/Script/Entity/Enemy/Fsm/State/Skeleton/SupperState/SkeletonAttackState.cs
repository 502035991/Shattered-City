using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SkeletonAttackState : EnemyState
{
    protected SkeletonEnemy enemy;

    private bool IsCritical;
    public SkeletonAttackState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = (SkeletonEnemy)EnemyBase;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if(!enemy.isControlled)
            enemy.SetVelocityX(0);
    }
    public override void AnimationFinishTrigger()
    {
        enemyStateMachine.ChangeState(enemy.battleState);
    }
    public override void SetAdditionalData(object value)
    {
        IsCritical = (bool)value;
        if (IsCritical)
            enemy.anim.SetTrigger("IsCritical");
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if(enemy.isAttacking)
        {
            enemy.UseAttackStatae();
            var coll = enemy.GetAttackTarget();

            if (coll == null)
                return;

            foreach (var item in coll)
            {
                Player target = item.GetComponent<Player>();
                if (target != null)
                {
                    target.TakeDamage();
                    if (IsCritical)
                    {
                        target.KnockBack(new Vector2(7 * -enemy.facingDirection, 10), 10).Forget();
                    }
                }

            }
        }
    }
}
