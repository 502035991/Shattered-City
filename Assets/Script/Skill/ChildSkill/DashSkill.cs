using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class DashSkill : Skill
{
    private bool isFirstDashed;
    [SerializeField] private float interval;//间隔时间
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
        // 如果此时没有再次按下 Dash 键，则进入冷却状态
        if (isFirstDashed == false)
            return;
        cooldownTimer = 1 - interval;
        isFirstDashed = false;                   
    }

}
