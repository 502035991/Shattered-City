using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGame : MonoBehaviour
{
    [SerializeField]private UI_IconImg dashSkill;
    [SerializeField]private UI_IconImg cloneSkill;

    private bool isDash;
    private bool isClone;

    private void Update()
    {
        isDash = PlayerManager.instance.player.inputHandler.isDash;
        isClone = PlayerManager.instance.player.inputHandler.isCloneDsah;

        if (isDash)
        {
            dashSkill.EnterCooldown();
        }
        if (isClone)
        {
            cloneSkill.EnterCooldown();
        }

        dashSkill.SetValue(SkillManager.instance.dash.GetCoolDownNorm());
        dashSkill.SetSliderValue(SkillManager.instance.dash.GetIntervalVlaue());

        cloneSkill.SetValue(SkillManager.instance.cloneDash.GetCoolDownNorm());
    }
    private void LateUpdate()
    {

    }
}
