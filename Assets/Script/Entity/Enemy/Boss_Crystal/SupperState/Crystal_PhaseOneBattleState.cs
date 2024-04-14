
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
    /// ��һ����ͨ�����󣬲�������ڲ��ڹ�����Χ�ڣ�������еڶ�����ͨ����,����ֻ��Ҫ�ж��Ƿ���Խ��е�һ����ͨ����
    /// ֻ�еڶ�����ͨ�����������Ż��ж��Ƿ�����ͷż���
    /// ���ڶ�����ͨ����CDת�ã������½��빥������
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
    /// ����
    /// </summary>
/*    private void CheckCanAttack()
    {
        float dis = Vector2.Distance(enemy.transform.position, player.transform.position);
        if (!enemy.CheckIsOnCooldown(CrystalCD.BaseAttack2))//�ж�Attack2 CD
        {
            checkWait = false;
            if (dis < enemyData.Skill[0].distance || enemy.CheckIsOnCooldown(CrystalCD.BaseAttack1))
            {
                //����ڹ�����������Attack1 , ���Attack1����CD�� ��Attack2
                enemyStateMachine.ChangeState(enemy.attackState);
            }
            else
                enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
        }
        else if(!checkWait)
        {
            checkWait = true;
            enemyStateMachine.ChangeState(enemy.idleState, 2f);//update�� ��ִֻ֤��һ��
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
