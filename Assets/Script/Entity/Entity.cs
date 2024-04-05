
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    left = -1,
    right = 1,
};
public enum Phase//½×¶Î
{ 
    One =1,
    Two =2,
    Three =3,
}

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public EntityFX entityFX { get; private set; }
    public CharacterStats stats { get; private set; }
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
    protected Vector3 baseAttackSize;
    [SerializeField]
    protected BaseData entityData;
    [SerializeField]
    private Direction imgDirection;

    protected bool isEnemy = true;

    [HideInInspector] public bool isAttacking = false;
    internal bool CanBeHurt = true;

    public Action onFlipped;

    #region CallBack
    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        RB = GetComponent<Rigidbody2D>();
        entityFX = GetComponent<EntityFX>();
        stats =GetComponent<CharacterStats>();
    }
    protected virtual void Start()
    {
        facingDirection = (int)imgDirection;
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
    #endregion
    #region Check
    public bool CheckIfTouchingGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.layerToGround);
    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, entityData.wallCheckDistance, entityData.layerToWall);
    }
    #endregion
    #region ¶¯×÷Êä³ö
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
    public void SetFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }
    #region Attack
    public virtual Collider2D[] GetAttackTarget()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(attackCheck.position, entityData.attackDistance);
        return coll;
    }
    public void AttackTarget()
    {
        isAttacking = true;
    }
    public void UseAttackState() => isAttacking = false;

    public void SetHurtState(bool value) => CanBeHurt = value;
    public virtual void TakeDamageEffect()
    {
        //entityFX.FlashFX().Forget();
    }
    #endregion
    internal void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0, 180, 0);

        onFlipped?.Invoke();
    }
    public virtual void Die()
    {

    }
}
