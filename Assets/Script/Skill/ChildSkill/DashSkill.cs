using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class DashSkill : Skill
{
    private int dashCounter;
    private bool isFirstDashed;
    private bool dashEnd;
    [SerializeField] private float interval;//���ʱ��
    public override bool CanUseSkill()
    {
        if (dashCounter > 1)
            dashCounter = 0;

        if (cooldownTimer <= 0)
        {
            dashCounter += 1;
            dashEnd = false;
            cooldownTimer = colldown;
            UseSkill();
            return true;           
        }
        else if(dashEnd && dashCounter == 1)
        {
            dashCounter += 1;
            dashEnd = false;
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
        dashEnd =true;
        await UniTask.Delay(TimeSpan.FromSeconds(interval));
        // �����ʱû���ٴΰ��� Dash ��,���ü�����
        if (dashCounter != 0)
            dashCounter = 0;                  
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log(cooldownTimer);
    }
}
