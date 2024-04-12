
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Crystal_AttackState : Crystal_AbilityState
{
    private int normalAttackConter = 1;

    public Crystal_AttackState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalAttackMenu> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
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
        if (value != null && value is CrystalAttackMenu)
        {
            if (value is CrystalAttackMenu.BaseAttack1)
                normalAttackConter = 1;
            else if (value is CrystalAttackMenu.BaseAttack2)
                normalAttackConter = 2;
        }
    }
    public override async void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemy.UseAttackState();
        if (enemy.currentPhase == Phase.One)
        {
            if (normalAttackConter == 1)
            {
                await UniTask.Delay(450);
                enemyStateMachine.ChangeState(enemy.oneBattleState);
                ac?.Invoke(CrystalAttackMenu.BaseAttack1);
            }
            else
            {
                await UniTask.Delay(600);
                enemyStateMachine.ChangeState(enemy.idleState, 1.5f);
                ac?.Invoke(CrystalAttackMenu.BaseAttack2);
            }
        }
    }
    public override void DoCheck()
    {
        base.DoCheck();
        if (enemy.isAttacking)
        {
            if (normalAttackConter == 1)
            {
                var coll = enemy.NewGetAttackTarget(CrystalAttackMenu.BaseAttack1);
                if (coll != null)
                {
                    enemy.UseAttackState();
                    enemy.SetVelocityX(0);

                    Player target = coll.GetComponent<Player>();
                    target.stats.DoDamage(player.stats, enemyData.Skill[CrystalAttackMenu.BaseAttack1].Damage);

                    target.KnockBack(7, enemy.facingDirection, 0.5f);
                }
            }
            else if(normalAttackConter ==2)
            {
                var coll = enemy.NewGetAttackTarget(CrystalAttackMenu.BaseAttack2);
                if (coll != null)
                {
                    enemy.UseAttackState();
                    enemy.SetVelocityX(0);

                    Player target = coll.GetComponent<Player>();
                    target.stats.DoDamage(player.stats, enemyData.Skill[CrystalAttackMenu.BaseAttack2].Damage);

                    target.KnockBack(12, enemy.facingDirection, 0.8f);
                }
            }
        }        
    }
}
