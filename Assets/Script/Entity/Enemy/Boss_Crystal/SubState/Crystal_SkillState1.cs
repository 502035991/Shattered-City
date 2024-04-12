using System;
using UnityEngine;
public class Crystal_SkillState1 : Crystal_AbilityState
{
    public Crystal_SkillState1(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalAttackMenu> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
    {
    }

    public override void AnimationSkillEffect()
    {
        var obj = enemy.CreatSkill(enemyData.Skill[CrystalAttackMenu.Skill_1].obj);
        var controller = obj.GetComponent<CrystalSkillRockController>();

        controller.Init(enemy.facingDirection , 0.7f , 10f , enemy.skillRockPos.position);

    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemyStateMachine.ChangeState(enemy.idleState, 3.5f);
        ac?.Invoke(CrystalAttackMenu.Skill_1);
    }
    public override void Exit()
    {
        base.Exit();
        isAbilityDone = true;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetInteger("SkillCounter", 1);
    }
}
