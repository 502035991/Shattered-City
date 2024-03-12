using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerAbilityState
{
    public PlayerPrimaryAttackState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    private int normalAttackConter = 0;
    public override async void AnimationFinishTrigger()
    {
        player.inputHandler.UseAttackInput();
        await WaitForAnimationCompletion();
        normalAttackConter++;
        await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        if (player.inputHandler.isAttack)
        {
            //stateMachine.ChangeState(this);
        }
        else
        {
            normalAttackConter = 0;
        }
    }   
    private async UniTask WaitForAnimationCompletion()
    {
        // 检测动画是否播放完毕
        while (player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            await UniTask.Yield();
        }
        isAbilityDone = true;

    }

    public override void Enter()
    {
        base.Enter();
        if (normalAttackConter > 2)
            normalAttackConter = 0;

        player.SetVelocityX(playerData.attacckMovement[normalAttackConter].x * player.facingDirection);
        //player.SetVelocityY(playerData.attacckMovement[normalAttackConter].y);

        player.anim.SetInteger("NormalAttackConter", normalAttackConter);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocityX(0);
    }
}
