using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneDashState : PlayerAbilityState
{
    public PlayerCloneDashState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    private bool isCloneDashEnable;

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }
    public override async void Enter()
    {
        base.Enter();
        isCloneDashEnable = player.inputHandler.isCloneDashEnable;
        if (!isCloneDashEnable)
        {
            player.RB.gravityScale = 0;
            player.skill.cloneDash.CreatClone(player.transform, player.facingDirection);
        }
        else
        {
            player.skill.cloneDash.ChangePositionToClone(player.transform);
            player.RB.gravityScale = 0;
            await UniTask.Delay(100);
            player.RB.gravityScale = 3.5f;
        }

    }
    public override void Exit()
    {
        base.Exit();
        if(!isCloneDashEnable)
        {
            player.inputHandler.UseCloneDashInput();
            player.RB.gravityScale = 3.5f;
        }
        else
        {
            player.inputHandler.UseCloneDashMoveInput();
        }


    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocity(Vector2.zero);
    }

    private void ChangePos()
    {

    }
}
