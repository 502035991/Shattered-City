using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void EnemyNormalAttackCallBack() => enemy.AnimationTrigger();
    private void EnemyNormalAttackGetPlayer() => enemy.AttackTarget();
}
