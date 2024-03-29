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

    private void EnemyAnimationEndCallBack() => enemy.AnimationTrigger();
    private void EnemySkillCreatObj() => enemy.AnimationSkillCreatObj();
    private void EnemyAttackCallBack() => enemy.AttackTarget();
}
