using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine enemyStateMachine;
    protected EnemyData enemyData;
    protected Enemy baseEnemy;

    protected Player player;

    private string animName;

    protected bool isDead =false;
    
    public EnemyState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Enemy enemy, string animName)
    {
        this.enemyStateMachine = enemyStateMachine;
        this.enemyData = enemyData;
        this.baseEnemy = enemy;
        this.animName = animName;
    }
    public virtual void Enter()
    {
        Debug.Log("enter " + enemyStateMachine.currentState + "  " + animName );
        baseEnemy.anim.SetBool(animName, true);

        if (player == null)
        {
            player = PlayerManager.instance.player;
        }
    }
    public virtual void Update()
    {            
    }
    public virtual void DoCheck()
    {
/*        if (baseEnemy.isControlled && !isDead)
            enemyStateMachine.ChangeState(baseEnemy.GetHitState());*/
    }
    public virtual void PhysicUpdate()
    {
    }
    public virtual void Exit()
    {
        //Debug.Log("exit "+ enemyStateMachine.currentState +"  "+ animName);
        baseEnemy.anim.SetBool(animName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        //动画结束事件
    }
    public virtual void AnimationSkillEffect()
    {
        //动画发技能帧事件
    }
    public virtual void SetAdditionalData(object value)
    {
        //状态复用 传不同参数进行控制
    }
}
