using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : Entity
{
    private PlayerData playerData;
    public PlayerInputHandler inputHandler { get; private set; }
    public PlayerStateMachine stateMachine {  get; private set; }
    public SkillManager skill {  get; private set; }
    public bool isControlled {  get; private set; }
    private float startControlTime;
    
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
    public async UniTask KnockBack(Vector2 direction, float magnitude, float duraton)
    {
        if (!isControlled)
        {
            SetVelocity(direction.normalized * magnitude);

            isControlled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(duraton)); // 例如无敌持续时间        
            isControlled = false;            
        }
    }
    public async void KnockBackUp(int direction ,float duration , float power)
    {
        if (!isControlled)
        {
            startControlTime = Time.time;
            SetFlip(-direction);
            isControlled = true;
            RB.AddForce(new Vector2(5 *direction , 3) * power, ForceMode2D.Impulse);
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            isControlled = false;
        }
    }
    private Sequence knockBackUpTween;
    private Tweener knockBackMove;
    public void KnockBackUp(int direction, float distance, float power, float durection)
    {
        if (!isControlled)
        {
            isControlled = true;
            SetFlip(-direction);
            var targetPos = new Vector2(transform.position.x + distance * direction, transform.position.y);
            knockBackUpTween = transform.DOJump(targetPos, power, 1, durection)
                .OnUpdate(() =>
                {
                    if (Physics2D.Raycast(transform.position, Vector3.right * -facingDirection, 1f, 1 << LayerMask.NameToLayer("Wall")))
                    {
                        knockBackUpTween.Kill();
                        isControlled = false;
                    }
                })
                .OnComplete(() => isControlled = false);
        }
    }


    public void KnockBackHor(int direction, float distance, float durection)
    {
        if (!isControlled)
        {
            isControlled = true;
            SetFlip(-direction);
            var targetPos = new Vector2(transform.position.x + distance * direction, transform.position.y);
            knockBackMove = transform.DOMove(targetPos,durection)
                .OnUpdate(() =>
                {
                    if (Physics2D.Raycast(transform.position, Vector3.right * -facingDirection, 0.5f, 1 << LayerMask.NameToLayer("Wall")))
                    {
                        knockBackMove.Kill();
                        isControlled = false;
                    }
                })
                .OnComplete(() => isControlled = false);
        }
    }
    /// <summary>
    /// 僵直，控制，吸附
    /// </summary>
    public void ControllPlyer(Vector2 pos ,int direction)
    {
        if (!isControlled)
        {
            isControlled = true;
            SetFlip(-direction);
            RB.gravityScale = 0;
        }
        transform.position = new Vector2(pos.x, transform.position.y);

    }

    public void CancelControl()
    {
        isControlled =false;
        RB.gravityScale = 3.5f;
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
        Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckRadius);
        Gizmos.DrawWireSphere(attackCheck.position, entityData.attackDistance);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + entityData.wallCheckDistance, wallCheck.position.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(airAttackChenck.position, baseAttackSize);
    }
}
