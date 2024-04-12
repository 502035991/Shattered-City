using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
public enum Phase//阶段
{
    One = 1,
    Two = 2,
    Three = 3,
}
public abstract class Enemy : Entity
{    
    public EnemyStateMachine stateMachine {  get; private set; }
    protected EnemyData enemyData { get; private set; }

    [SerializeField]
    protected Transform playerCheck;


    [SerializeField]
    private bool canControl;
    public CapsuleCollider2D coll {  get; private set; }

    #region CallBack
    protected override void Awake()
    {
        base.Awake();
        coll = GetComponent<CapsuleCollider2D>();
        stateMachine = new EnemyStateMachine();
        enemyData = (EnemyData)entityData;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    protected override void LateUpdate()
    {
        stateMachine.currentState.DoCheck();
    }
    protected override void FixedUpdate()
    {
        stateMachine.currentState.PhysicUpdate();
    }
    #endregion
    #region Check
    public bool CheckIfPlayerToLine(out RaycastHit2D hitResult)
    {
        var hitLeft = Physics2D.Raycast(playerCheck.position, Vector3.left, enemyData.playerCheckLength, enemyData.layerToPlayer);
        if (hitLeft.collider != null)
        {
            hitResult = hitLeft;
            return true;
        }
        else
        {
            var hitRight = Physics2D.Raycast(playerCheck.position, Vector3.right, enemyData.playerCheckLength, enemyData.layerToPlayer);
            if (hitRight.collider != null)
            {
                hitResult = hitRight;
                return true; 
            }
            else
            {
                hitResult = default;
                return false; 
            }
        }
    }
    public bool CheckIfPlayerToCircle(out Collider2D hitResult , float radius)
    {
        var hitTarget = Physics2D.OverlapCircle(transform.position,radius,enemyData.layerToPlayer);
        if (hitTarget != null)
        {
            hitResult = hitTarget;
            return true;
        }
        else
        {
            hitResult = default;
            return false;
        }

    }

    #endregion
    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
    public void AnimationSkillCreatObj()
    {
        stateMachine.currentState.AnimationSkillEffect();
    }
    public virtual async UniTask KnockBack(Vector2 direction, float magnitude, float duraton)
    {
        if (!isControlled && canControl)
        {
            SetVelocity(direction.normalized * magnitude);

            isControlled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(duraton)); // 例如无敌持续时间        
            isControlled = false;
        }
    }
    public abstract EnemyState GetHitState();


}
