using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Boss_Crystal;
using static UnityEngine.EventSystems.EventTrigger;

public enum CrystalCD
{
    BaseAttack1,
    BaseAttack2,
    Skill1,
    Skill2,
}
public class Boss_Crystal : Enemy
{
    #region State
    public Crystal_IdleSate idleState {  get; private set; }
    public Crystal_InAirState inAirState { get; private set; }
    public Crystal_JumpState jumpState { get; private set; }
    public Crystal_LandState landState { get; private set; }
    public Crystal_MoveState moveState { get; private set; }

    //¹¥»÷
    public Crystal_SkillState1 skillState1{ get; private set; }
    public Crystal_AttackState attackState { get; private set; }
    #endregion
    #region ±äÁ¿
    public Phase currentPhase {  get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        moveState = new Crystal_MoveState(stateMachine, enemyData, this, "Move");
        idleState = new Crystal_IdleSate(stateMachine, enemyData, this, "Idle");
        landState = new Crystal_LandState(stateMachine, enemyData, this, "Land");


        inAirState = new Crystal_InAirState(stateMachine, enemyData, this, "Jump");
        jumpState = new Crystal_JumpState(stateMachine, enemyData, this, "Jump", GetCurrentAttack);
        attackState = new Crystal_AttackState(stateMachine, enemyData, this, "Attack", GetCurrentAttack);
        skillState1 = new Crystal_SkillState1(stateMachine, enemyData, this, "SkillOne", GetCurrentAttack);

        //deadState = new SlimeDeadState(stateMachine, enemyData, this, "Dead");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        currentPhase = Phase.One;

        foreach(var sa in enemyData.Skill)
        {
            if(!CdList.ContainsKey(sa.name))
                CdList.Add(sa.name, sa.CD);
        }
    }
    #region Attack
    private Dictionary<CrystalCD ,float> CdList = new Dictionary<CrystalCD ,float>();
    private List<CrystalCD> checkCDs = new List<CrystalCD>();
    public bool CheckIsOnCooldown(CrystalCD attackName)
    {
        return checkCDs.Contains(attackName);
    }
    private void GetCurrentAttack(CrystalCD attackName)
    {
        if (!checkCDs.Contains(attackName))
        {
            checkCDs.Add(attackName);
            CooldownTimer(attackName, CdList[attackName]);
        }
    }
    private async void CooldownTimer(CrystalCD attackName, float cooldown)
    {
        while (cooldown >0)
        {
            cooldown -= Time.deltaTime;
            await UniTask.Yield();
        }
        checkCDs.Remove(attackName);
    }
    #endregion
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
