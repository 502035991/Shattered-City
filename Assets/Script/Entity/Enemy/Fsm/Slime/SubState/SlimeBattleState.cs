using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBattleState : SlimeBaseState
{
    private float attackTimer;
    private bool lastStateIsBattle;

    public SlimeBattleState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void DoCheck()
    {
        base.DoCheck();
        CheckFilp();

        if (!enemy.isControlled && attackTimer < 0)
        {
            if (IsAttackedToPlayer())//¼ì²âÍæ¼ÒÊÇ·ñÔÚ¹¥»÷·¶Î§ÄÚ
            {
                attackTimer = enemyData.attackCD;
                enemy.SetVelocityX(0);
                enemyStateMachine.ChangeState(enemy.attackState);                
            }
            else
            {
                enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
            }
        }
        else
        {
            if(attackTimer * 2 > enemyData.attackCD && lastStateIsBattle)            
                enemy.SetVelocityX(-enemy.facingDirection * enemyData.movementVelocity);            
            else            
                enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);            
        }
    }
    public override void Update()
    {
        base.Update();
        attackTimer -= Time.deltaTime;
    }
    public override void SetAdditionalData(object value)
    {
        if (value != null)
            lastStateIsBattle =(bool)value;
        else
            lastStateIsBattle = false;
    }


    private bool IsAttackedToPlayer()
    {
        RaycastHit2D hitResult;
        enemy.CheckIfPlayerToLine(out hitResult);
        return (hitResult.point - new Vector2(enemy.transform.position.x, enemy.transform.position.y)).sqrMagnitude < enemyData.attackDistance;
    }
    private void CheckFilp()
    {
        RaycastHit2D hitResult;
        if (enemy.CheckIfPlayerToLine(out hitResult))
        {
            enemy.SetFilp(hitResult.point.x < enemy.transform.position.x ? -1 : 1);
        }
        else
        {
            enemyStateMachine.ChangeState(enemy.moveState);
        }
    }

}
