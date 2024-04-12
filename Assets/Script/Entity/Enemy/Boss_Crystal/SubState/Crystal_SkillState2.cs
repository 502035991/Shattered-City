using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_SkillState2 : Crystal_AbilityState
{
    public Crystal_SkillState2(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalAttackMenu> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
    {
    }

    public override void AnimationSkillEffect()
    {
        var obj = enemy.CreatSkill(enemyData.Skill[CrystalAttackMenu.Skill_2].obj);
        var controller = obj.GetComponent<CrystalSkillRock2Controller>();

        controller.Init(enemy.facingDirection, enemy.skillRockPos.position);
        //controller.Init(enemy.facingDirection, 0.7f, 10f, enemy.skillRockPos.position);

    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemyStateMachine.ChangeState(enemy.idleState, 3.5f);
        ac?.Invoke(CrystalAttackMenu.Skill_2);
    }
    public override void Exit()
    {
        base.Exit();
        isAbilityDone = true;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetInteger("SkillCounter", 2);
    }
}
