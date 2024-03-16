using Cysharp.Threading.Tasks;
using UnityEngine;

public class DashSkill : Skill
{
    private bool isFirstDashed;
    private float dashCooldownTimer;

    protected override void Update()
    {
        base.Update();

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }
    public override bool CanUseSkill()
    { 
        if (dashCooldownTimer <= 0)
        {
            if (!isFirstDashed)
            {
                isFirstDashed = true;
                cooldownTimer = colldown;//���ܻ���CD���ã������ͻ
                UseSkill();
                return true;
            }
            else if (isFirstDashed && !PlayerManager.instance.player.inputHandler.isDash)
            {
                isFirstDashed= false;
                dashCooldownTimer = colldown;
                cooldownTimer = colldown;
                return true;
            }
        }
        return false;
    }

    public override async void UseSkill()
    {
        await UniTask.WaitUntil(() => !PlayerManager.instance.player.inputHandler.isDash);
        await UniTask.Delay(400);
        // �����ʱû���ٴΰ��� Dash �����������ȴ״̬
        dashCooldownTimer = 0.6f;
        isFirstDashed = false;
            
        
    }

}
