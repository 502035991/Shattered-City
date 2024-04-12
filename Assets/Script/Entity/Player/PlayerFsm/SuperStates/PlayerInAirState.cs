using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isDash;   
    private bool isAirAttack;

    private bool isCloneDash;
    private bool isCloneDashEnable;
    private bool isTimeStopEnable;


    public PlayerInAirState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = player.CheckIfTouchingGround();
        isTouchingWall = player.CheckIfTouchingWall();
        xInput = player.inputHandler.normInputX;
        isDash = player.inputHandler.isDash;
        isAirAttack = player.inputHandler.isAirAttack;

        isCloneDash = player.inputHandler.isCloneDsah;
        isCloneDashEnable = player.inputHandler.isCloneDashEnable;
        isTimeStopEnable = player.inputHandler.isTimeStopEnable;

    }

    public override void Enter()
    {
        base.Enter();
        isGrounded = false;
        isTouchingWall = false;
        xInput = 0;

        player.inputHandler.SetAirState(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.inputHandler.UseJumpInput();
        player.inputHandler.SetAirState(false);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if(xInput !=  0)
        {
            player.SetFlip(xInput);
            player.SetVelocityX(xInput * playerData.movementVelocity);
        }
    }

    public override void Update()
    {
        base.Update();
        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if(isTouchingWall && xInput == player.facingDirection && player.currentVelocity.y < 0.01f)
        {
            //stateMachine.ChangeState(player.wallSlideState);
        }
        else if(isAirAttack)
        {
            stateMachine.ChangeState(player.airAttackState);
        }
        else if(isDash)
        {
            isDash = false;
            stateMachine.ChangeState(player.dashState);
        }
        else if(isCloneDash || isCloneDashEnable)
        {
            isCloneDashEnable = false;
            isCloneDash = false;
            stateMachine.ChangeState(player.CloneDashState);
        }
        else if (isTimeStopEnable)
        {
            isTimeStopEnable = false;
            stateMachine.ChangeState(player.timeStopState ,bState.air);
        }
        else
        {
            player.anim.SetFloat("yVelocity", player.currentVelocity.y);
        }
    }
}
