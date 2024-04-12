
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
public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public EntityFX entityFX { get; private set; }
    public CharacterStats stats { get; private set; }
    #endregion
    private Vector2 workspace;
    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }
    public bool isAttacking { get; private set; }

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

    protected bool CanBeHurt = true;
    protected bool isControlled = false;

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
        return Physics2D.Raycast(groundCheck.position, Vector2.down, entityData.groundCheckDistance, entityData.layerToGround);
    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, entityData.wallCheckDistance, entityData.layerToWall);
    }
    public bool CheckIfTouchingGroudToHeight(float height)
    {
        return Physics2D.Raycast(groundCheck.position , Vector2.down, height, entityData.layerToGround);
    }
    #endregion
    #region ¶¯×÷Êä³ö
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, currentVelocity.y);
        SetFinalVelocity();
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(currentVelocity.x, velocity);
        SetFinalVelocity();
    }
    public void SetVelocity(Vector2 velocity)
    {
        workspace = velocity;
        SetFinalVelocity();
    }
    public void SetFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }
    private void SetFinalVelocity()
    {
        if(!isControlled)
        {
            RB.velocity = workspace;
            currentVelocity = workspace;
        }    
    }
    internal void Flip()
    {
        if (!isControlled)
        {
            facingDirection *= -1;
            transform.Rotate(0, 180, 0);

            onFlipped?.Invoke();
        }
    }
    #endregion
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
    public virtual void Die()
    {

    }
}
