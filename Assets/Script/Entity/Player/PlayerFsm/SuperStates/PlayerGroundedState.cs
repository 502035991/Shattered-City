using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;

    private bool jumpInput;
    private bool isDash;
    private bool isAttack;
    public PlayerGroundedState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = player.inputHandler.normInputX;
        jumpInput = player.inputHandler.jumpInput;
        isDash = player.inputHandler.isDash;
        isAttack = player.inputHandler.isAttack;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        xInput = player.inputHandler.normInputX;
        jumpInput = player.inputHandler.jumpInput;
        isDash = player.inputHandler.isDash;
        isAttack = player.inputHandler.isAttack;
    }
    public override void PhysicUpdate()    {
        base.PhysicUpdate();

        if(jumpInput)
        {
            jumpInput = false;
            stateMachine.ChangeState(player.jumpState);
        }
        else if(isDash)
        {
            isDash = false;
            stateMachine.ChangeState(player.dashState);
        }
        else if(isAttack)
        {
            isAttack = false;
            stateMachine.ChangeState(player.playerPrimaryAttackState);
        }
    }
}