using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_PhaseTwoBattleState : EnemyState
{
    private Boss_Crystal enemy;
    private bool checkWait = false;
    public Crystal_PhaseTwoBattleState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = enemy;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        CheckAttack();
    }
    private void CheckAttack()
    {
        var currentSkill = ReleaseRandomSkill();

        /*        if (!enemy.CheckIsOnCooldown(CrystalAttackMenu.Jump_1))
                    enemyStateMachine.ChangeState(enemy.jumpState, 1);
                else
                    enemyStateMachine.ChangeState(enemy.jumpState, 2);*/
        switch (currentSkill)
        {
            case CrystalAttackMenu.Jump_1:
                if (!enemy.CheckIsOnCooldown(CrystalAttackMenu.Jump_1))
                    enemyStateMachine.ChangeState(enemy.jumpState, 1);
                return;
            case CrystalAttackMenu.Jump_2:
                if (!enemy.CheckIsOnCooldown(CrystalAttackMenu.Jump_2))
                    enemyStateMachine.ChangeState(enemy.jumpState, 2);
                return;
            case CrystalAttackMenu.Skill_1:
                if (!enemy.CheckIsOnCooldown(CrystalAttackMenu.Skill_1))
                    enemyStateMachine.ChangeState(enemy.skillState1);
                return;
            case CrystalAttackMenu.Skill_2:
                if (!enemy.CheckIsOnCooldown(CrystalAttackMenu.Skill_2))
                    enemyStateMachine.ChangeState(enemy.skillState2);
                return;
        }
    }
    private CrystalAttackMenu ReleaseRandomSkill()
    {
        var skillDictionary = enemyData.Skill;

        // 计算总权重
        float totalWeight = 0f;
        foreach (var skill in skillDictionary.Values)
        {
            totalWeight += skill.weight;
        }
        // 生成随机数
        float randomValue = Random.Range(0f, totalWeight);

        // 根据随机数选择技能
        float cumulativeWeight = 0f;
        foreach (var skill in skillDictionary.Values)
        {
            cumulativeWeight += skill.weight;
            if (randomValue <= cumulativeWeight)
            {
                return skill.name;
            }
        }
        return default;
    }
}
