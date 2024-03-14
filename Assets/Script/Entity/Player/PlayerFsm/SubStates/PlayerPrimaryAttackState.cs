using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerAbilityState
{
    public PlayerPrimaryAttackState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    private int normalAttackConter = 0;
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
                //标记是否进行了攻击
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

    public override void DoCheck()
    {
        base.DoCheck();
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
                    //target.stateMachine.ChangeState(target.GetStunnedState());
                    if (normalAttackConter == 1)
                    {
                        target.KnockBack(new Vector2(0, 10), 10).Forget();
                    }
                    else if (normalAttackConter == 2)
                    {
                        target.KnockBack(new Vector2(7 * player.facingDirection, 15), 10).Forget();
                    }
                }

            }                
            
        }
    }
}
