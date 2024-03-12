using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;

    private bool isGrounded;
    private bool isDashed;
    public PlayerAbilityState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {

    }
    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
        isGrounded = player.CheckIfTouchingGround();
        isDashed = player.inputHandler.isDash;
    }
    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = player.CheckIfTouchingGround();
        isDashed = player.inputHandler.isDash;
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
