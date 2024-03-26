using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bState
{ 
    air = 0,
    ground = 1,
}
public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected Player player;

    private string animName;

    public PlayerState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName)
    {
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.player = player;
        this.animName = animName;
    } 

    public virtual void Enter()
    {
        //Debug.Log("enter Player " + animName);
        player.anim.SetBool(animName, true);
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
        //Debug.Log("exit Player" + animName);
        player.anim.SetBool(animName, false);
    }
    public virtual void SetAdditionalData(object value)
    {

    }
    public virtual void AnimationFinishTrigger()
    {

    }
}
