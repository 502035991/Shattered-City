using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;

    protected bool isGrounded;
    protected bool isDash;
    public PlayerAbilityState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {

    }
    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
        isGrounded = player.CheckIfTouchingGround();
        isDash = player.inputHandler.isDash;
    }
    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = player.CheckIfTouchingGround();
        isDash = player.inputHandler.isDash;
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

        if (isAbilityDone)
        {
             if (isGrounded && player.currentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.inAirState);
            } 
        }
    }
}
