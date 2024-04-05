using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_GroundedState : EnemyState
{
    protected Boss_Crystal enemy;

    public Crystal_GroundedState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void DoCheck()
    {
        base.DoCheck();
        CheckFilp();
    }
    private void CheckFilp()
    {
        if(!enemy.CheckIsOnCooldown(CrystalCD.BaseAttack1) || !enemy.CheckIsOnCooldown(CrystalCD.BaseAttack2))
            enemy.SetFlip(player.transform.position.x < enemy.transform.position.x ? -1 : 1);
    }

}
