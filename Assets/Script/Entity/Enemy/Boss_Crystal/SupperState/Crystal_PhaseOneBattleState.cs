
using UnityEngine;

public class Crystal_PhaseOneBattleState : EnemyState
{
    private Boss_Crystal enemy;
    private bool checkWait =false;

    public Crystal_PhaseOneBattleState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = enemy;
    }

    private bool canUseSkill = false;
    public override void DoCheck()
    {
        base.DoCheck();

        NewCheckAttack();
    }
    /// <summary>
    /// 第一段普通攻击后，不管玩家在不在攻击范围内，都会进行第二段普通攻击,所以只需要判断是否可以进行第一段普通攻击
    /// 只有第二段普通攻击结束，才会判断是否可以释放技能
    /// 当第二段普通攻击CD转好，则重新进入攻击次序
    /// </summary>
    private void NewCheckAttack()
    {
        float dis = Vector2.Distance(enemy.transform.position, player.transform.position);
        
        if(canUseSkill)
        {
            if (!enemy.CheckIsOnCooldown(CrystalAttackMenu.Skill_1) && dis < enemyData.Skill[CrystalAttackMenu.Skill_1].distance)
            {
                enemyStateMachine.ChangeState(enemy.skillState1);
                canUseSkill = false;
            }
            else if(!enemy.CheckIsOnCooldown(CrystalAttackMenu.BaseAttack2))
            {
                canUseSkill = false;
            }
            else
            {
                enemy.SetFlip(player.transform.position.x < enemy.transform.position.x ? -1 : 1);
                enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
            }
        }
        else
        {
            if (dis < enemyData.Skill[CrystalAttackMenu.BaseAttack1].distance && !enemy.CheckIsOnCooldown(CrystalAttackMenu.BaseAttack1))
            {
                enemyStateMachine.ChangeState(enemy.attackState, CrystalAttackMenu.BaseAttack1);//Attack_1
                canUseSkill = false;
            }
            else if (enemy.CheckIsOnCooldown(CrystalAttackMenu.BaseAttack1))
            {
                enemyStateMachine.ChangeState(enemy.attackState, CrystalAttackMenu.BaseAttack2);//Attack_2
                canUseSkill = true;
            }
            else
            {
                enemy.SetFlip(player.transform.position.x < enemy.transform.position.x ? -1 : 1);
                enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
            }
        }
    }

    /// <summary>
    /// 弃用
    /// </summary>
/*    private void CheckCanAttack()
    {
        float dis = Vector2.Distance(enemy.transform.position, player.transform.position);
        if (!enemy.CheckIsOnCooldown(CrystalCD.BaseAttack2))//判断Attack2 CD
        {
            checkWait = false;
            if (dis < enemyData.Skill[0].distance || enemy.CheckIsOnCooldown(CrystalCD.BaseAttack1))
            {
                //如果在攻击距离内则Attack1 , 如果Attack1进入CD了 则Attack2
                enemyStateMachine.ChangeState(enemy.attackState);
            }
            else
                enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
        }
        else if(!checkWait)
        {
            checkWait = true;
            enemyStateMachine.ChangeState(enemy.idleState, 2f);//update里 保证只执行一次
        }
        else
        {            
            if(dis < enemyData.Skill[2].distance)
            {
                enemyStateMachine.ChangeState(enemy.skillState1);
            }
            else
            {
                enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
            }
        }
    }*/
}
