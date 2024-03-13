using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine enemyStateMachine;
    protected EnemyData enemyData;
    protected Enemy EnemyBase;

    private string animName;

    public EnemyState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName)
    {
        this.enemyStateMachine = enemyStateMachine;
        this.enemyData = enemyData;
        this.EnemyBase = enemy;
        this.animName = animName;
    }
    public virtual void Enter()
    {
        Debug.Log("enter " + enemyStateMachine.currentState + "  " + animName );
        EnemyBase.anim.SetBool(animName, true);
    }
    public virtual void Update()
    {
    }
    public virtual void DoCheck()
    {
    }
    public virtual void PhysicUpdate()
    {
    }
    public virtual void Exit()
    {
        Debug.Log("exit "+ enemyStateMachine.currentState +"  "+ animName);
        EnemyBase.anim.SetBool(animName, false);
    }
    public virtual void AnimationFinishTrigger()
    {

    }
    public virtual void SetAdditionalData(object value)
    {

    }
}
