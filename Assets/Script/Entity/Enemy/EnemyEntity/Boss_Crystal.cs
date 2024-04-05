using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

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

    //攻击
    public Crystal_SkillState1 skillState1{ get; private set; }
    public Crystal_AttackState attackState { get; private set; }
    public Crystal_DeadState deadState { get; private set; }
    public Crystal_ChangePhaseState changePhaseState { get; private set; }
    #endregion
    #region 变量
    public Phase currentPhase {  get; private set; }
    [SerializeField]
    private Transform skillRockPos;
    public AnimationCurve jumpCurve;
    #endregion

    public float jumpDuration;
    public float jumpMaxHeight;
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.W))
        {
            stateMachine.ChangeState(jumpState);
        }
    }
    protected override void Awake()
    {
        base.Awake();
        moveState = new Crystal_MoveState(stateMachine, enemyData, this, "Move");
        idleState = new Crystal_IdleSate(stateMachine, enemyData, this, "Idle");
        landState = new Crystal_LandState(stateMachine, enemyData, this, "Land", GetCurrentCD);
         
        inAirState = new Crystal_InAirState(stateMachine, enemyData, this, "Jump");
        jumpState = new Crystal_JumpState(stateMachine, enemyData, this, "Jump", GetCurrentCD);
        attackState = new Crystal_AttackState(stateMachine, enemyData, this, "Attack", GetCurrentCD);
        skillState1 = new Crystal_SkillState1(stateMachine, enemyData, this, "Skill", GetCurrentCD);
        changePhaseState = new Crystal_ChangePhaseState(stateMachine, enemyData, this,"ChangePhase", GetCurrentCD);

        deadState = new Crystal_DeadState(stateMachine, enemyData, this, "Die", GetCurrentCD);

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
    /// <summary>
    /// CD检查
    /// </summary>
    public bool CheckIsOnCooldown(CrystalCD attackName)
    {
        return checkCDs.Contains(attackName);        
    }
    /// <summary>
    /// 当前攻击/技能 计入CD
    /// </summary>
    private void GetCurrentCD(CrystalCD attackName)
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
    private async void CooldownTimer(CrystalCD attackName, float cooldown)
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

        if(currentPhase == Phase.One)
            obj1.transform.position = skillRockPos.position;


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
