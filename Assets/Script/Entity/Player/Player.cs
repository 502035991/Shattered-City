using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class Player : Entity
{
    private PlayerData playerData;
    public PlayerInputHandler inputHandler { get; private set; }
    public PlayerStateMachine stateMachine {  get; private set; }
    public SkillManager skill {  get; private set; }
    private float startControlTime;

    public CinemachineImpulseSource MyInpulse;
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
        inputHandler = GetComponentInChildren<PlayerInputHandler>();

        MyInpulse = GetComponent<CinemachineImpulseSource>();
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
    //被击退
    public async void KnockBack(float knockbackDis , int enemyDir , float duration , bool isForcedAttack =false)
    {
        if(isForcedAttack)
            isControlled = false;

        if(!isControlled)
        {          
            SetFlip(-enemyDir);
            RB.gravityScale = 3.5f;
            var velocity = CalculateVelocity(knockbackDis, enemyDir, duration);
            //RB.AddForce(velocity, ForceMode2D.Impulse);
            SetVelocity(velocity);

            isControlled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            while(true)
            {
                if (CheckIfTouchingGround())
                {
                    isControlled = false;
                    SetVelocityX(0);
                    return;
                }
                await UniTask.Yield();
            }
        }
    }
    //计算初始速度
    private Vector2 CalculateVelocity(float distance ,int enemyDir, float duration)
    {
        var gravitySum = Physics2D.gravity * RB.gravityScale;
        var targetPos = new Vector2(transform.position.x + enemyDir * distance, transform.position.y);
        Vector2 startPos = transform.transform.position;

        Vector2 knockbackVelocity = (targetPos - startPos) / duration - 0.5f * gravitySum * duration;
        //knockbackVelocity.x *= enemyDir;
        return knockbackVelocity;
    }
/*    private Sequence knockBackUpTween;
    private Tweener knockBackMove;
    public void KnockBackUp(int direction, float distance, float power, float durection)
    {
        if (CanBeHurt && !isControlled)
        {
            SetControlStats(true);
            SetFlip(-direction);
            var targetPos = new Vector2(transform.position.x + distance * direction, transform.position.y);
            knockBackUpTween = transform.DOJump(targetPos, power, 1, durection)
                .OnUpdate(() =>
                {
                    if (Physics2D.Raycast(transform.position, Vector3.right * -facingDirection, 1f, 1 << LayerMask.NameToLayer("Wall")))
                    {
                        knockBackUpTween.Kill();
                        SetControlStats(false);
                    }
                })
                .OnComplete(() => SetControlStats(false));
        }
    }
    public void KnockBackHor(int direction, float distance, float durection)
    {
        if (CanBeHurt && !isControlled)
        {
            SetControlStats(true);
            SetFlip(-direction);
            var targetPos = new Vector2(transform.position.x + distance * direction, transform.position.y);
            knockBackMove = transform.DOMove(targetPos,durection)
                .OnUpdate(() =>
                {
                    if (Physics2D.Raycast(transform.position, Vector3.right * -facingDirection, 0.5f, 1 << LayerMask.NameToLayer("Wall")))
                    {
                        knockBackMove.Kill();
                        SetControlStats(false);
                    }
                })
                .OnComplete(() => SetControlStats(false));
        }
    }*/
    public void SetControl()
    {
        isControlled = true;
    }
    public void CancelControl()
    {
        if (!isControlled)
            return;
        isControlled=false;
    }
    public bool GetControlState()
    {
        return isControlled;
    }
    [SerializeField]
    private Transform airAttackChenck;
    public Collider2D[] GetAirAttackTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(airAttackChenck.position, baseAttackSize, 0f);
        return colliders;
    }
    public override void Die()
    {
        stateMachine.ChangeState(deadState);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckDistance);
        Gizmos.DrawWireSphere(attackCheck.position, entityData.attackDistance);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + entityData.wallCheckDistance, wallCheck.position.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(airAttackChenck.position, baseAttackSize);
    }
}
