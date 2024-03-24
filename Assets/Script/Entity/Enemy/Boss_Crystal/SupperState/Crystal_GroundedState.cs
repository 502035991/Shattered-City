using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_GroundedState : EnemyState
{
    protected Boss_Crystal enemy;

    public Crystal_GroundedState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = (Boss_Crystal)enemy;
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
        enemy.SetFilp(player.transform.position.x < enemy.transform.position.x ? -1 : 1);
    }

}
