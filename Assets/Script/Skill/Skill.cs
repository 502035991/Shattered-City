using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float colldown;
    protected float cooldownTimer;

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer < 0)
        {
            cooldownTimer = colldown;
            UseSkill();
            return true;
        }
        return false;
    }

    public virtual void UseSkill()
    {

    }
}
