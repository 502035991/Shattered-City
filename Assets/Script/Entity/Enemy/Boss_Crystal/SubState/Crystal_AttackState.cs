
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Crystal_AttackState : Crystal_AbilityState
{
    private int normalAttackConter = 1;

    public Crystal_AttackState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalCD> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (enemy.currentPhase == Phase.One)
        {
            if (normalAttackConter == 1)
                enemy.SetVelocityX(10 * enemy.facingDirection);
            else
                enemy.SetVelocityX(15 * enemy.facingDirection);
        }
        else
        {

        }
        enemy.anim.SetInteger("NormalAttackConter", normalAttackConter);
    }
    public override void SetAdditionalData(object value)
    {
        if(value is int)
        {
            normalAttackConter = (int)value;
        }
    }
    public override async void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        if (normalAttackConter == 1)
        {
            await UniTask.Delay(450);
            enemyStateMachine.ChangeState(enemy.oneBattleState);
            ac?.Invoke(CrystalCD.BaseAttack1);
        }
        else
        {
            await UniTask.Delay(600);
            enemyStateMachine.ChangeState(enemy.idleState, 1.3f);
            ac?.Invoke(CrystalCD.BaseAttack2);
        }
    }
    public override void CheckAttackTarget()
    {
        if (!player.CanBeHurt)
            return;
        float dis = Vector2.Distance(enemy.transform.position, player.transform.position);

        if (normalAttackConter == 1 && dis < enemyData.Skill[0].distance)
        {
            player.stats.DoDamage(player.stats, enemyData.Skill[0].Damage);
            player.KnockBackHor(enemy.facingDirection, 5, 0.5f);
        }
        else if (dis < enemyData.Skill[1].distance)
        {
            player.stats.DoDamage(player.stats, enemyData.Skill[1].Damage);
            player.KnockBackUp(enemy.facingDirection, 15, 2f, 0.5f);
        }
    }
    /// <summary>
    /// ÆúÓÃ
    /// </summary>
    private void AttackPlayer()
    {
        float dis = Vector2.Distance(enemy.transform.position, player.transform.position);
        if (enemy.isAttacking)
        {
            enemy.UseAttackState();
            if (!player.CanBeHurt)
                return;
            if (normalAttackConter == 1 && dis < enemyData.Skill[0].distance)
            {
                player.stats.DoDamage(player.stats, enemyData.Skill[0].Damage);
                player.KnockBackHor(enemy.facingDirection, 5, 0.5f);
            }
            else if(dis < enemyData.Skill[1].distance)
            {
                player.stats.DoDamage(player.stats, enemyData.Skill[1].Damage);
                player.KnockBackUp(enemy.facingDirection , 15, 2f ,0.5f);
            }
        }
    }

}
