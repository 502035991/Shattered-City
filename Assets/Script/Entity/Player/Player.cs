using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Entity
{
    public static Player Instance;
    private PlayerData playerData;
    public PlayerInputHandler inputHandler { get; private set; }
    public PlayerStateMachine stateMachine {  get; private set; }
    public SkillManager skill {  get; private set; }
    #region States
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandState landState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerAttackState playerPrimaryAttackState { get; private set; }

    //¼¼ÄÜ
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
        playerPrimaryAttackState = new PlayerAttackState(stateMachine, playerData, this, "Attack");

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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckRadius);
        Gizmos.DrawWireSphere(attackCheck.position, entityData.attackCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + entityData.wallCheckDistance, wallCheck.position.y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
