using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocityY(-playerData.wallSlideVelocity);
    }
}
