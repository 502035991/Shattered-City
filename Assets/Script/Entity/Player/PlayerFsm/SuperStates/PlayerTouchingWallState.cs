using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isDash;

    private float xInput;
    public PlayerTouchingWallState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = player.CheckIfTouchingGround();
        isTouchingWall = player.CheckIfTouchingWall();
        isDash = player.inputHandler.isDash;
        xInput = player.inputHandler.normInputX;
        if(isGrounded)
        {
            Debug.Log("ss");
        }
    }

    public override void Enter()
    {
        base.Enter();
        isGrounded = false;
        isTouchingWall = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void Update()
    {
        base.Update();
        if(isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if(!isTouchingWall || xInput != player.facingDirection)
        {
            stateMachine.ChangeState(player.inAirState);
        }

        if(isDash)
        {
            player.inputHandler.UseDashInput();
        }
    }
}
