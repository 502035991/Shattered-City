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
        player.RB.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.inputHandler.UseDashInput();
        player.RB.gravityScale = 3.5f;
        player.SetVelocityX(0);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if (timer < 0)
        {
            isAbilityDone = true;
            player.SetHurtState(true);//ÎÞµÐ½áÊø
        }
        else
        {
            player.SetVelocity(new Vector2(playerData.dashVelocity * player.facingDirection, 0));
            player.SetHurtState(false);//ÎÞµÐ
        }
    }
    public override void Update()
    {
        base.Update();
        timer -=Time.deltaTime;

    }
}
