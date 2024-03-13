using UnityEngine;

public class SkeletonBattleState : SkeletonGroundedState
{
    public SkeletonBattleState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }
    private float attackTimer;
    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
    }
    public override void DoCheck()
    {
        base.DoCheck();
        RaycastHit2D hitResult;
        if (enemy.CheckIfPlayerToLine(out hitResult))
        {
            enemy.SetFilp(hitResult.point.x < enemy.transform.position.x ? -1 : 1);
        }
        attackTimer -= Time.deltaTime;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        if (IsAttackedToPlayer() && !enemy.isHurt)
        {
            enemy.SetVelocityX(0);
            enemy.anim.SetBool("Move", false);
        }
        else if (!enemy.isHurt)
        {
            enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
            enemy.anim.SetBool("Move", true);
        }
        else
        {

        }
    }

    public override void Update()
    {
        base.Update();
        if (!enemy.CheckIfPlayerToLine(out _))
        {
            enemyStateMachine.ChangeState(enemy.idleState);
        }
        else if (IsAttackedToPlayer() && attackTimer < 0)
        {
            float value = Random.Range(0f, 1f);
            attackTimer = enemyData.attackCD;
            enemyStateMachine.ChangeState(enemy.attackState ,value < enemyData.CriticalValue);
        }
    }

    private bool IsAttackedToPlayer()
    {
        RaycastHit2D hitResult;
        enemy.CheckIfPlayerToLine(out hitResult);
        return (hitResult.point - new Vector2(enemy.transform.position.x, enemy.transform.position.y)).sqrMagnitude < enemyData.attackDistance;
    }
}
