
using UnityEngine;

public class Crystal_MoveState : Crystal_GroundedState
{

    private bool checkWait =false;

    public Crystal_MoveState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        CheckCanAttack();
    }


    private void CheckCanAttack()
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
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }
}
