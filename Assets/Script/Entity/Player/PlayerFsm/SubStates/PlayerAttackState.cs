using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    public PlayerAttackState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    private int normalAttackConter = 0;
    private float lastAttackTime;
    private float intervaltime = 0.8f;

    private CancellationTokenSource cancellationTokenSource;

    public override void Enter()
    {
        base.Enter();
        if (normalAttackConter > 2 || Time.time > lastAttackTime + intervaltime)
            normalAttackConter = 0;

        player.SetVelocityX(playerData.attacckMovement[normalAttackConter].x * player.facingDirection);

        player.anim.SetInteger("NormalAttackConter", normalAttackConter);
    }
    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
        // 取消等待动画完成的操作
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocityX(0);
    }
    public override void DoCheck()
    {
        base.DoCheck();
        AttackTarget();
    }
    private void AttackTarget()
    {        
        if (player.isAttacking)
        {
            player.UseAttackState();
            var coll = player.GetAttackTarget();

            if (coll == null)
                return;

            foreach (var item in coll)
            {
                Enemy target = item.GetComponent<Enemy>();
                if (target != null)
                {

                    if (normalAttackConter == 0)
                    {
                        player.stats.DoDamage(target.stats ,1);
                        target.KnockBack(Vector2.zero, 0 ,0.3f).Forget();//玩家攻击1
                    }
                    else if (normalAttackConter == 1)
                    {
                        player.stats.DoDamage(target.stats ,3);
                        target.KnockBack(new Vector2(0, 10), 10,0.3f).Forget();//玩家攻击2
                    }
                    else if (normalAttackConter == 2)
                    {
                        player.stats.DoDamage(target.stats ,5);
                        target.KnockBack(new Vector2(7 * player.facingDirection, 15), 10,0.5f).Forget();//玩家攻击3
                    }
                }
            }
            normalAttackConter++;
        }
    }
    public override async void AnimationFinishTrigger()
    {
        player.inputHandler.UseAttackInput();

        cancellationTokenSource = new CancellationTokenSource();

        try
        {
            await WaitForAnimationCompletion(cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {

        }
    }
    private async UniTask WaitForAnimationCompletion(CancellationToken cancellationToken)
    {
        bool isAttackInput =false;
        // 检测动画是否播放完毕，同时监听取消操作
        while (!cancellationToken.IsCancellationRequested && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            //如果预输入阶段按下了攻击键 则记录攻击状态
            if(player.inputHandler.isAttack && !isAttackInput)
                isAttackInput = true;


            await UniTask.Yield();
        }
        // 如果取消操作被触发，则直接抛出 OperationCanceledException 异常
        cancellationToken.ThrowIfCancellationRequested();

        if (isAttackInput)
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else
        {
            isAbilityDone = true;
        }
    }
}
