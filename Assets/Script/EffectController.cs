using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effect
{
    Hit,
    ChangePhase,
}

public class EffectController : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetEffect(Effect ef)
    {
        switch (ef)
        {
        case Effect.Hit:
            anim.SetBool("Hit", true);        
            break;
        case Effect.ChangePhase:
            anim.SetBool("ChangePhase", true);
            break;
        }
    }

    public void AnimFinishTriggerr()
    {
        Destroy(gameObject);
    }

}
