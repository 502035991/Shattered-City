
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public EntityFX entityFX { get; private set; }
    #endregion

    private Vector2 workSpace;
    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }

    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected Transform attackCheck;
    [SerializeField]
    protected BaseData entityData;

    protected bool isEnemy = true;//e
    [HideInInspector] public bool isControlled = false;
    [HideInInspector] public bool isAttacking = false;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        RB = GetComponent<Rigidbody2D>();
        entityFX = GetComponent<EntityFX>();
    }
    protected virtual void Start()
    {
        facingDirection = 1;
    }
    protected virtual void Update()
    {
        currentVelocity = RB.velocity;
    }
    protected virtual void LateUpdate()
    {

    }
    protected virtual void FixedUpdate()
    {

    }
    #region Check
    public bool CheckIfTouchingGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.layerToGround);
    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, entityData.wallCheckDistance, entityData.layerToGround);
    }
    #endregion
    #region 动作输出
    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, currentVelocity.y);
        RB.velocity = new Vector2(velocity, currentVelocity.y);
        currentVelocity = workSpace;
    }
    public void SetVelocityY(float velocity)
    {
        workSpace.Set(currentVelocity.x, velocity);
        RB.velocity = workSpace;
        currentVelocity = workSpace;
    }
    public void SetVelocity(Vector2 velocity)
    {
        workSpace = velocity;
        RB.velocity = workSpace;
        currentVelocity = workSpace;
    }
    #endregion
    public void SetFilp(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Filp();
        }
    }
    #region Attack
    public Collider2D[] GetAttackTarget()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(attackCheck.position, entityData.attackCheckRadius);
        return coll;
    }
    public void AttackTarget()
    {
        isAttacking = true;
    }
    public void UseAttackStatae() => isAttacking = false;
    public virtual void TakeDamage()
    {
        //entityFX.FlashFX().Forget();
    }
    public virtual async UniTask KnockBack(Vector2 direction, float magnitude)
    {
        if(!isControlled)
        {
            SetVelocity(direction.normalized * magnitude);

            isControlled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f)); // 例如无敌持续时间        
            isControlled = false;
        }
    }
    #endregion
    internal void Filp()
    {
        facingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }
}
