using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void PlayerNormalAttackCallBack() => player.AnimationTrigger();
    private void PlayerNormalAttackGetEnemy() => player.AttackTarget();

    
}
