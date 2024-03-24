using System.Collections.Generic;
using UnityEngine;


public class Boss_Crystal : Enemy
{
    public enum CrystalCD
    {
        BaseAttack,
        Skill1,
        Skill2,
    }
    #region State
    public Dictionary<string, float> attackCooldowns = new Dictionary<string, float>(); // 攻击冷却时间
    public Crystal_IdleSate idleState {  get; private set; }
    public Crystal_InAirState inAirState { get; private set; }
    public Crystal_JumpState jumpState { get; private set; }
    public Crystal_LandState landState { get; private set; }
    public Crystal_AttackState attackState { get; private set; }
    public Crystal_MoveState moveState { get; private set; }
    #endregion
    #region 变量
    public Phase currentPhase {  get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        attackState = new Crystal_AttackState(stateMachine, enemyData, this, "Attack");
        moveState = new Crystal_MoveState(stateMachine, enemyData, this, "Move");
        //battleState = new SlimeBattleState(stateMachine, enemyData, this, "Move");
        idleState = new Crystal_IdleSate(stateMachine, enemyData, this, "Idle");
        landState = new Crystal_LandState(stateMachine, enemyData, this, "Land");
        jumpState = new Crystal_JumpState(stateMachine, enemyData, this, "Jump");
        inAirState = new Crystal_InAirState(stateMachine, enemyData, this, "Jump");
        //deadState = new SlimeDeadState(stateMachine, enemyData, this, "Dead");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        currentPhase = Phase.One;
    }
    public void SetPhase(int va)
    {
        currentPhase = (Phase)va;
    }
    public override Collider2D[] GetAttackTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackCheck.position, baseAttackSize, 0f);
        return colliders;
    }
    public override EnemyState GetHitState()
    {
        throw new System.NotImplementedException();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackCheck.position, baseAttackSize);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(attackCheck.position, entityData.attackDistance * Vector3.right);
    }

}
