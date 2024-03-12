using UnityEngine;

public class SkeletonEnemy : Enemy
{
    #region State
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake(); 
        idleState = new SkeletonIdleState(stateMachine,enemyData,this,"Idle");
        moveState = new SkeletonMoveState(stateMachine, enemyData, this, "Move");
        battleState = new SkeletonBattleState(stateMachine, enemyData, this, "Move");
        attackState = new SkeletonAttackState(stateMachine, enemyData, this, "Attack");        
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected void OnDrawGizmos()
    {
        var da = (EnemyData)entityData;
        Vector2 leftBoundary = playerCheck.position + Vector3.left * da.playerCheckLength;
        Vector2 rightBoundary = playerCheck.position + Vector3.right * da.playerCheckLength;

        Gizmos.DrawLine(leftBoundary, rightBoundary);
    }
}
