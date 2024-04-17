using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class DashSkill : Skill
{
    private int dashCounter;
    private bool isFirstDashed;
    private bool CanDash;
    [SerializeField] private float interval;//¼ä¸ôÊ±¼ä
    private float intervalTimer = 1;
    public override bool CanUseSkill()
    {
        if (dashCounter > 1)
            dashCounter = 0;

        if (cooldownTimer <= 0)
        {
            dashCounter += 1;
            CanDash = false;
            cooldownTimer = colldown;
            UseSkill();
            return true;           
        }
        else if(CanDash && dashCounter == 1)
        {
            dashCounter += 1;
            CanDash = false;
            cooldownTimer = colldown;
            return true;
        }
        else
        {
            return false; 
        }
    }
    public override async void UseSkill()
    {
        await UniTask.WaitUntil(() => !PlayerManager.instance.player.inputHandler.isDashed);
        CanDash = true;
        intervalTimer = 0;
        await UniTask.Delay(TimeSpan.FromSeconds(interval));
        if (CanDash)
        {
            CanDash = false;
            dashCounter = 0;
        }
    }
    public float GetIntervalVlaue()
    {
        return intervalTimer / interval;
    }

    protected override void Update()
    {
        base.Update();
        if (intervalTimer < interval)
        {
            intervalTimer += Time.deltaTime;
        }

        Debug.Log(cooldownTimer);
    }
}
