using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class DashSkill : Skill
{
    private bool isFirstDashed;
    [SerializeField] private float interval;//���ʱ��
    public override bool CanUseSkill()
    { 
        if (cooldownTimer <= 0)
        {
            if (!isFirstDashed)
            {
                isFirstDashed = true;
                UseSkill();
                return true;
            }
            else if (isFirstDashed && !PlayerManager.instance.player.inputHandler.isDash)
            {
                isFirstDashed= false;
                cooldownTimer = colldown;
                return true;
            }
        }
        return false;
    }

    public override async void UseSkill()
    {
        await UniTask.WaitUntil(() => !PlayerManager.instance.player.inputHandler.isDash);
        await UniTask.Delay(TimeSpan.FromSeconds(interval));
        // �����ʱû���ٴΰ��� Dash �����������ȴ״̬
        if (isFirstDashed == false)
            return;
        cooldownTimer = 1 - interval;
        isFirstDashed = false;                   
    }

}
