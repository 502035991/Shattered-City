using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class DashSkill : Skill
{
    private int dashCounter;
    private bool isFirstDashed;
    private bool CanDash;
    [SerializeField] private float interval;//¼ä¸ôÊ±¼ä
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
        await UniTask.WaitUntil(() => !PlayerManager.instance.player.inputHandler.isDash);
        CanDash =true;
        await UniTask.Delay(TimeSpan.FromSeconds(interval));
        if(CanDash)
        {
            CanDash = false;
            dashCounter = 0;
        }
               
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log(cooldownTimer);
    }
}
