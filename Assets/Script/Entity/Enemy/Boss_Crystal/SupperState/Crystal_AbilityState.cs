using System;
public class Crystal_AbilityState : EnemyState
{
    protected Boss_Crystal enemy;
    protected Action<CrystalAttackMenu> ac;

    protected bool isAbilityDone;
    protected bool isGrounded;
    public Crystal_AbilityState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalAttackMenu> ac) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = enemy;
        this.ac = ac;
    }
    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
        enemy.SetFlip(player.transform.position.x < enemy.transform.position.x ? -1 : 1);
    }
    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = enemy.CheckIfTouchingGround();
        if (isAbilityDone)
        {
            if (isGrounded && enemy.currentVelocity.y < 0.01f)
            {
                enemyStateMachine.ChangeState(enemy.idleState);
            }
            else
            {
                enemyStateMachine.ChangeState(enemy.inAirState);
            }
        }

    }
}
