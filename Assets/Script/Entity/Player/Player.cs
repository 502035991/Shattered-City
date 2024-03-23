using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Player : Entity
{
    private PlayerData playerData;
    public PlayerInputHandler inputHandler { get; private set; }
    public PlayerStateMachine stateMachine {  get; private set; }
    public SkillManager skill {  get; private set; }
    [HideInInspector] public bool isControlled = false;

    #region States
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandState landState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerHitState hitState { get; private set; }

    public PlayerDeadState deadState { get; private set; }

    //技能
    public PlayerAttackState primaryAttackState { get; private set; }
    public PlayerAirAttackState airAttackState { get; private set; }
    public PlayerCloneDashState CloneDashState { get; private set; }
    public PlayerTimeStopState timeStopState { get; private set; }
    #endregion
    #region CallBack
    protected override void Awake()
    {
        base.Awake();
        isEnemy = false;
        stateMachine = new PlayerStateMachine();
        playerData = (PlayerData)entityData;

        idleState = new PlayerIdleState(stateMachine, playerData, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, playerData, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, playerData, this, "InAir");
        dashState = new PlayerDashState(stateMachine, playerData, this, "Dash");
        inAirState = new PlayerInAirState(stateMachine, playerData, this, "InAir");        
        landState = new PlayerLandState(stateMachine, playerData, this, "Land");
        wallSlideState = new PlayerWallSlideState(stateMachine, playerData, this, "WallSlide");
        hitState = new PlayerHitState(stateMachine, playerData, this, "Hit");
        primaryAttackState = new PlayerAttackState(stateMachine, playerData, this, "Attack");
        airAttackState = new PlayerAirAttackState(stateMachine, playerData, this, "AirAttack");
        deadState = new PlayerDeadState(stateMachine, playerData, this, "Dead");

        CloneDashState = new PlayerCloneDashState(stateMachine, playerData, this, "CloneDash");
        timeStopState = new PlayerTimeStopState(stateMachine, playerData, this, "TimeStop");
    }
    protected override void Start()
    {
        base.Start();
        inputHandler = GetComponent<PlayerInputHandler>();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    protected override void LateUpdate()
    {
        stateMachine.currentState.DoCheck();
    }
    protected override void FixedUpdate()
    {
        stateMachine.currentState.PhysicUpdate();
    }
    #endregion
    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
    public virtual async UniTask KnockBack(Vector2 direction, float magnitude, float duraton)
    {
        if (!isControlled)
        {
            SetVelocity(direction.normalized * magnitude);

            isControlled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(duraton)); // 例如无敌持续时间        
            isControlled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckRadius);
        Gizmos.DrawWireSphere(attackCheck.position, entityData.attackDistance);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + entityData.wallCheckDistance, wallCheck.position.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(airAttackChenck.position, size);
    }
    public Vector3 size;
    [SerializeField]
    private Transform airAttackChenck;

    public Collider2D[] GetAirAttackTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(airAttackChenck.position, size, 0f);
        return colliders;
    }
    public override void Die()
    {
        stateMachine.ChangeState(deadState);
    }
}
