using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float timer;
    public PlayerDashState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = playerData.duration;
    }

    public override void Exit()
    {
        base.Exit();
        player.inputHandler.UseDashInput();
        player.SetVelocityX(0);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void Update()
    {
        base.Update();
        timer -=Time.deltaTime;
        if(timer < 0 )
        {
            isAbilityDone = true;
        }
        else
        {
            player.SetDashVelocityX( playerData.dashVelocity * player.facingDirection);
        }
    }
}
