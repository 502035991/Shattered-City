using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClongDashState : PlayerAbilityState
{
    public PlayerClongDashState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }
    public override void Enter()
    {
        base.Enter();

        player.RB.gravityScale = 0;
        player.skill.clone.CreatClong(player.transform , player.facingDirection);  
    }
    public override void Exit()
    {
        base.Exit();
        player.inputHandler.UseClongDashInput();
        player.RB.gravityScale = 3.5f;
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocity(Vector2.zero);
    }
}
