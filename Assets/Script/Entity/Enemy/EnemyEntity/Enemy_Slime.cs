using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : Enemy
{
    #region State
    public SlimeAttackState attackState {  get; private set; }
    public SlimeDeadState deadState {  get; private set; }
    public SlimeHitState hitState {  get; private set; }
    public SlimeIdleState idleState { get; private set; }
    public SlimeMoveState moveState { get; private set; }

    public SlimeBattleState battleState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        attackState = new SlimeAttackState(stateMachine, enemyData, this, "Attack");
        moveState = new SlimeMoveState(stateMachine, enemyData, this, "Move");
        battleState = new SlimeBattleState(stateMachine, enemyData, this, "Move");
        hitState = new SlimeHitState(stateMachine, enemyData, this, "Hit");
        idleState = new SlimeIdleState(stateMachine, enemyData, this, "Idle");
        deadState = new SlimeDeadState(stateMachine, enemyData, this, "Dead");

    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    public override void Die()
    {
        stateMachine.ChangeState(deadState);
    }
    public override EnemyState GetHitState()
    {
        return hitState;
    }
    protected void OnDrawGizmos()
    {
        var da = (EnemyData)entityData;
        Vector2 leftBoundary = playerCheck.position + Vector3.left * da.playerCheckLength;
        Vector2 rightBoundary = playerCheck.position + Vector3.right * da.playerCheckLength;

        Gizmos.DrawWireSphere(attackCheck.position, entityData.attackDistance);//¼ì²â¹¥»÷¾àÀë
        Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(wallCheck.position, entityData.wallCheckDistance * Vector3.right);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(leftBoundary, rightBoundary);//¼ì²âÍæ¼Ò
    }

}
