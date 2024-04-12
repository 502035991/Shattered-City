using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public enum CrystalAttackMenu
{
    BaseAttack1,
    BaseAttack2,
    Skill_1,
    Skill_2,
    Jump_1,
    Jump_2,
}
public class Boss_Crystal : Enemy
{
    #region State
    public Crystal_IdleSate idleState {  get; private set; }
    public Crystal_InAirState inAirState { get; private set; }
    public Crystal_JumpState jumpState { get; private set; }
    public Crystal_LandState landState { get; private set; }

    public Crystal_PhaseOneBattleState oneBattleState { get; private set; }
    public Crystal_PhaseTwoBattleState twoBattleState { get; private set; }

    //攻击
    public Crystal_SkillState1 skillState1{ get; private set; }
    public Crystal_SkillState2 skillState2 { get; private set; }
    public Crystal_AttackState attackState { get; private set; }
    public Crystal_DeadState deadState { get; private set; }
    public Crystal_ChangePhaseState changePhaseState { get; private set; }
    #endregion
    #region 变量
    public Phase currentPhase {  get; private set; }
    public Transform skillRockPos;
    public Transform skillLandPos;

    [SerializeField]
    private Vector3 baseAttackSize_2;
    #endregion
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentPhase = Phase.Two;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        oneBattleState = new Crystal_PhaseOneBattleState(stateMachine, enemyData, this, "Move");
        twoBattleState = new Crystal_PhaseTwoBattleState(stateMachine, enemyData, this, "Idle");
        idleState = new Crystal_IdleSate(stateMachine, enemyData, this, "Idle");
        landState = new Crystal_LandState(stateMachine, enemyData, this, "Land");

         
        inAirState = new Crystal_InAirState(stateMachine, enemyData, this, "Jump");
        jumpState = new Crystal_JumpState(stateMachine, enemyData, this, "Jump", GetCurrentCD);
        attackState = new Crystal_AttackState(stateMachine, enemyData, this, "Attack", GetCurrentCD);
        skillState1 = new Crystal_SkillState1(stateMachine, enemyData, this, "Skill", GetCurrentCD);
        skillState2 = new Crystal_SkillState2(stateMachine, enemyData, this, "Skill", GetCurrentCD);

        changePhaseState = new Crystal_ChangePhaseState(stateMachine, enemyData, this,"ChangePhase");

        deadState = new Crystal_DeadState(stateMachine, enemyData, this, "Die");

        //deadState = new SlimeDeadState(stateMachine, enemyData, this, "Dead");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        currentPhase = Phase.One;

        foreach(var skill in enemyData.AllAttack)
        {
            if(!CdList.ContainsKey(skill.name))
                CdList.Add(skill.name, skill.CD);
        }
    }
    #region Attack
    private Dictionary<CrystalAttackMenu ,float> CdList = new Dictionary<CrystalAttackMenu ,float>();
    private List<CrystalAttackMenu> checkCDs = new List<CrystalAttackMenu>();
    /// <summary>
    /// CD检查
    /// </summary>
    public bool CheckIsOnCooldown(CrystalAttackMenu attackName)
    {
        return checkCDs.Contains(attackName);        
    }
    /// <summary>
    /// 当前攻击/技能 计入CD
    /// </summary>
    private void GetCurrentCD(CrystalAttackMenu attackName)
    {
        if (!checkCDs.Contains(attackName))
        {
            checkCDs.Add(attackName);
            CooldownTimer(attackName, CdList[attackName]);
        }
    }
    /// <summary>
    /// CD计时器
    /// </summary>
    private async void CooldownTimer(CrystalAttackMenu attackName, float cooldown)
    {
        while (cooldown >0)
        {
            cooldown -= Time.deltaTime;
            await UniTask.Yield();
        }
        checkCDs.Remove(attackName);
    }
    /// <summary>
    /// 放技能
    /// </summary>
    public GameObject CreatSkill(GameObject obj)
    {
        if (obj == null) return default;
        var obj1 =Instantiate(obj);
        return obj1;
    }
    #endregion
    public void SetPhase(Phase va)
    {
        currentPhase = va;
    }
    public override void Die()
    {
        if(currentPhase == Phase.Two)
            stateMachine.ChangeState(deadState);
        else
            stateMachine.ChangeState(changePhaseState);
    }
    public override Collider2D[] GetAttackTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackCheck.position, baseAttackSize, 0f);
        return colliders;
    }
    public Collider2D NewGetAttackTarget(CrystalAttackMenu value)
    {
        switch(value)
        {
            case CrystalAttackMenu.BaseAttack1:
                return Physics2D.OverlapBox(attackCheck.position, baseAttackSize, 0f,enemyData.layerToPlayer);
            case CrystalAttackMenu.BaseAttack2:
                return Physics2D.OverlapBox(attackCheck.position, baseAttackSize_2, 0f,enemyData.layerToPlayer);
            default:
                return default;
        }
    }
    public override EnemyState GetHitState()
    {
        throw new System.NotImplementedException();
    }
    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackCheck.position, baseAttackSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackCheck.position, baseAttackSize_2);
        
    }

}
