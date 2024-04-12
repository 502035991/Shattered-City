using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerAbilityState
{
    public PlayerAirAttackState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        player.anim.SetFloat("yVelocity", player.currentVelocity.y);
        HurtTarget();
    }

    public override void Enter()
    {
        base.Enter();
        AttackEffect();
    }

    public override void Exit()
    {
        base.Exit();
        player.inputHandler.UseAirAttackInput();
        player.RB.gravityScale = 3.5f;
    }
    private async void AttackEffect()
    {
        player.SetVelocity(new Vector2(0,2));
        player.RB.gravityScale = 0;
        await UniTask.Delay(250);
        player.SetVelocityY(-15);
    }
    private async void HurtTarget()
    {
        if(player.isAttacking)
        {
            player.UseAttackState();
            var coll = player.GetAirAttackTarget();

            if (coll == null)
                return;

            foreach (var item in coll)
            {
                if (item == null)
                    continue;
                Enemy target = item.GetComponent<Enemy>();
                if (target != null)
                {
                    player.stats.DoDamage(target.stats, 10);
                    target.KnockBack(Vector2.zero, 0, 0.3f).Forget();


                    player.SetVelocityY(0);
                    await UniTask.Delay(200);
                    player.SetVelocityY(-30);
                }
            }
            await UniTask.WaitUntil(() => isGrounded);
            await UniTask.Delay(250);
            isAbilityDone = true;
        }
    }
}
