using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
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

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocityX(playerData.movementVelocity * xInput);
        player.SetFilp(xInput);
    }

    public override void Update()
    {
        base.Update();
        if(xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
