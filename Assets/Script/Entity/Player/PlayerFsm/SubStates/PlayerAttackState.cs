using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    public PlayerAttackState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    private int normalAttackConter = 0;

    public override void Enter()
    {
        base.Enter();
        if (normalAttackConter > 2)
            normalAttackConter = 0;

        player.SetVelocityX(playerData.attacckMovement[normalAttackConter].x * player.facingDirection);

        player.anim.SetInteger("NormalAttackConter", normalAttackConter);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.SetVelocityX(0);
    }
    public override void DoCheck()
    {
        base.DoCheck();
        Attack();
    }
    private void Attack()
    {
        if (player.isAttacking)
        {
            player.UseAttackStatae();
            var coll = player.GetAttackTarget();

            if (coll == null)
                return;

            foreach (var item in coll)
            {
                Enemy target = item.GetComponent<Enemy>();
                if (target != null)
                {
                    target.TakeDamage();
                    if (normalAttackConter == 0)
                    {
                        target.KnockBack(Vector2.zero, 0).Forget();//Íæ¼Ò¹¥»÷1
                    }
                    else if (normalAttackConter == 1)
                    {
                        target.KnockBack(new Vector2(0, 10), 10).Forget();//Íæ¼Ò¹¥»÷2
                    }
                    else if (normalAttackConter == 2)
                    {
                        target.KnockBack(new Vector2(7 * player.facingDirection, 15), 10).Forget();//Íæ¼Ò¹¥»÷3
                    }
                }
            }
        }
    }
    public override async void AnimationFinishTrigger()
    {
        float startTime = Time.time;
        float currentTime = startTime;
        bool isAttacking = false;

        player.inputHandler.UseAttackInput();
        await WaitForAnimationCompletion();

        normalAttackConter++;
        while (currentTime - startTime < 0.6f)
        {
            currentTime = Time.time;
            if (player.inputHandler.isAttack)
            {
                //±ê¼ÇÊÇ·ñ½øÐÐÁË¹¥»÷
                isAttacking = true;
                break;
            }
            await UniTask.Yield();
        }
        if (!isAttacking)
            normalAttackConter = 0;
    }
    private async UniTask WaitForAnimationCompletion()
    {
        // ¼ì²â¶¯»­ÊÇ·ñ²¥·ÅÍê±Ï
        while (player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            await UniTask.Yield();
        }
        isAbilityDone = true;
    }

}
