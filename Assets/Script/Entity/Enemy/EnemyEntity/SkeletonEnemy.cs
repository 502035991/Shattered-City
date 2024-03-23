using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkeletonEnemy : Enemy
{
    #region State
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }
    public SkeletonDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake(); 
        idleState = new SkeletonIdleState(stateMachine,enemyData,this,"Idle");
        moveState = new SkeletonMoveState(stateMachine, enemyData, this, "Move");
        battleState = new SkeletonBattleState(stateMachine, enemyData, this, "Move");
        attackState = new SkeletonAttackState(stateMachine, enemyData, this, "Attack");
        stunnedState = new SkeletonStunnedState(stateMachine, enemyData, this, "Stunned");
        deadState = new SkeletonDeadState(stateMachine, enemyData, this, "Dead");
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

        Gizmos.DrawWireSphere(attackCheck.position, entityData.attackDistance);
        Gizmos.DrawLine(leftBoundary, rightBoundary);
    }

    public override EnemyState GetHitState()
    {
        return stunnedState;
    }

    public override UniTask KnockBack(Vector2 direction, float magnitude ,float duration)
    {
        isControlled = false;
        return base.KnockBack(direction, magnitude, duration);
    }
    public override void Die()
    {
        stateMachine.ChangeState(deadState);
    }
}
