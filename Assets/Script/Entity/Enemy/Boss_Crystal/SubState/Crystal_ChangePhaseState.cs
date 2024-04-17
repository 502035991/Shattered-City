using UnityEngine;

public class Crystal_ChangePhaseState : Crystal_GroundedState
{

    private float duration = 5f;
    private float startTime;

    private bool canResetHp = true;

    public Crystal_ChangePhaseState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetPhase(Phase.Two);
        enemy.SetHurtState(false);

        enemy.changePhaseTimeLine.Play();
        duration = (float)enemy.changePhaseTimeLine.duration;

        startTime = Time.time;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if(Time.time > startTime + duration)
        {
            enemyStateMachine.ChangeState(enemy.idleState, 1.5f);
        }
        else if(Time.time > startTime + 4 && canResetHp)
        {
            canResetHp = false;
            enemy.stats.ResetCurrentHealth();
        }
    }
    

    public override void Exit() 
    { 
        base.Exit();
        enemy.SetHurtState(true); 
    }
}
