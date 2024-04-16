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
        // ȡ���ȴ�������ɵĲ���
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
                        target.KnockBack(Vector2.zero, 0 ,0.3f).Forget();//��ҹ���1
                    }
                    else if (normalAttackConter == 1)
                    {
                        player.stats.DoDamage(target.stats ,3);
                        target.KnockBack(new Vector2(0, 10), 10,0.3f).Forget();//��ҹ���2
                    }
                    else if (normalAttackConter == 2)
                    {
                        player.stats.DoDamage(target.stats ,5);
                        target.KnockBack(new Vector2(7 * player.facingDirection, 15), 10,0.5f).Forget();//��ҹ���3
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
        // ��⶯���Ƿ񲥷���ϣ�ͬʱ����ȡ������
        while (!cancellationToken.IsCancellationRequested && player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            //���Ԥ����׶ΰ����˹����� ���¼����״̬
            if(player.inputHandler.isAttack && !isAttackInput)
                isAttackInput = true;


            await UniTask.Yield();
        }
        // ���ȡ����������������ֱ���׳� OperationCanceledException �쳣
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
