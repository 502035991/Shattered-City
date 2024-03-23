using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SlimeAttackState : SlimeBaseState
{
    public SlimeAttackState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }
    public override void AnimationFinishTrigger()
    {
        enemyStateMachine.ChangeState(enemy.battleState, true);
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public override void DoCheck()
    {
        base.DoCheck();
        AttackPlayerAsync();
    }
    
    private void AttackPlayerAsync()
    {
        if (enemy.isAttacking)
        {
            enemy.UseAttackState();

            var coll = enemy.GetAttackTarget();

            if (coll == null)
                return;

            foreach (var item in coll)
            {
                Player target = item.GetComponent<Player>();
                if (target != null)
                {
                    enemy.stats.DoDamage(target.stats, 2);
                    target.KnockBack(Vector2.zero, 0, 0.5f).Forget();
                    return;
                }
            }
        }
    }
}
