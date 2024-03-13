using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{    
    public EnemyStateMachine stateMachine {  get; private set; }
    protected EnemyData enemyData { get; private set; }

    [SerializeField]
    protected Transform playerCheck;

    public string curState;
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        enemyData = (EnemyData)entityData;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        curState = stateMachine.currentState .ToString();
    }
    protected override void LateUpdate()
    {
        stateMachine.currentState.DoCheck();
    }
    protected override void FixedUpdate()
    {
        stateMachine.currentState.PhysicUpdate();
    }
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


    public bool CheckIfPlayerToCircle()
    {
        return true;
    }

    #endregion
    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
}
