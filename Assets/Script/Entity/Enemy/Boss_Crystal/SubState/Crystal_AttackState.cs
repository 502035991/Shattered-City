using Autodesk.Fbx;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
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
        if (normalAttackConter > 2)
            normalAttackConter = 1;

        if(normalAttackConter ==1)
            enemy.SetVelocityX(10 * enemy.facingDirection);
        else
            enemy.SetVelocityX(15 * enemy.facingDirection);


        enemy.anim.SetInteger("NormalAttackConter", normalAttackConter);
    }
    public override async void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        if (normalAttackConter <2)
        {
            await UniTask.Delay(600);
            enemyStateMachine.ChangeState(enemy.idleState);
            ac?.Invoke(CrystalCD.BaseAttack1);
        }
        else
        {
            await UniTask.Delay(600);
            isAbilityDone = true;
            ac?.Invoke(CrystalCD.BaseAttack2);
        }
        normalAttackConter += 1;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        float dis = Vector2.Distance(enemy.transform.position, player.transform.position);
        if (enemy.isAttacking)
        {
            enemy.UseAttackState();
            if (normalAttackConter < 2 && dis < enemyData.Skill[0].distance)
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
