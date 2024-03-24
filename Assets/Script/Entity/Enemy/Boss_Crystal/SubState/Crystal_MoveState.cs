
public class Crystal_MoveState : Crystal_GroundedState
{
    public Crystal_MoveState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        float dis = (enemy.transform.position - player.transform.position).sqrMagnitude; 
        if(dis < enemyData.attackDistance)
        {
            enemyStateMachine.ChangeState(enemy.attackState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        enemy.SetVelocityX(enemy.facingDirection * enemyData.movementVelocity);
    }
}
