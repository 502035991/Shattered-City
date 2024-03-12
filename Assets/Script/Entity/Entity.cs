using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion


    private Vector2 workSpace;
    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }


    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected BaseData entityData;


    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        RB = GetComponent<Rigidbody2D>();
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
    public void SetDashVelocityX(float velocity)
    {
        workSpace.Set(velocity, 0);
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
    internal void Filp()
    {
        facingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }
}
