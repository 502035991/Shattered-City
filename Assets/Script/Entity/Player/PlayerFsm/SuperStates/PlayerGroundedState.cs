using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;

    private bool jumpInput;
    private bool isDash;
    private bool isAttack;

    private bool isCloneDash;
    private bool isCloneDashEnable;
    private bool isTimeStopEnable;
    public PlayerGroundedState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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

        isCloneDash = player.inputHandler.isCloneDsah;
        isCloneDashEnable = player.inputHandler.isCloneDashEnable;
        isTimeStopEnable = player.inputHandler.isTimeStopEnable;
    }
    public override void PhysicUpdate()    {
        base.PhysicUpdate();
        if(jumpInput)
        {
            jumpInput = false;
            stateMachine.ChangeState(player.jumpState);
        }
        else if (isAttack)
        {
            isAttack = false;
            stateMachine.ChangeState(player.primaryAttackState);
        }

        else if(isDash)
        {
            isDash = false;
            stateMachine.ChangeState(player.dashState);
        }
        else if (isCloneDash || isCloneDashEnable)
        {
            isCloneDash = false;
            isCloneDashEnable =false;
            stateMachine.ChangeState(player.CloneDashState);
        }
        else if(isTimeStopEnable)
        {
            isTimeStopEnable = false;
            stateMachine.ChangeState(player.timeStopState,bState.ground);
        }
    }
    public override void DoCheck()
    {
        base.DoCheck();

        if (player.isControlled)
            stateMachine.ChangeState(player.hitState);

    }
}
