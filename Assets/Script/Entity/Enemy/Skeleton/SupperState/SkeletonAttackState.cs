using UnityEngine;
using Cysharp.Threading.Tasks;

public class SkeletonAttackState : EnemyState
{
    protected Enemy_Skeleton enemy;

    private bool IsCritical;
    public SkeletonAttackState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
        this.enemy = (Enemy_Skeleton)base.baseEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        float value = Random.Range(0f, 1f);
        IsCritical = value < 0.25f;
        if (IsCritical)
            enemy.anim.SetTrigger("IsCritical");
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public override void AnimationFinishTrigger()
    {
        enemyStateMachine.ChangeState(enemy.battleState);
    }
    public override void DoCheck()
    {
        base.DoCheck();
        if(enemy.isAttacking)
        {
            enemy.UseAttackState();
            var coll = enemy.GetAttackTarget();

            if (coll == null)
                return;

            foreach (var item in coll)
            {
                Player target = item.GetComponent<Player>();
                if (target != null)
                {
/*                    target.TakeDamageEffect();
                    target.stats.TakeDamage();*/
                    if (IsCritical)
                    {
                        enemy.stats.DoDamage(target.stats, 5);
                        //target.KnockBack(new Vector2(7 * -enemy.facingDirection, 10), 10 ,0.5f).Forget();//���ñ���
                    }
                    else
                    {
                        enemy.stats.DoDamage(target.stats, 2);
                    }
                    return;
                }
            }
        }
    }
}
