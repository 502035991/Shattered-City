using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeStopState : PlayerAbilityState
{
    public PlayerTimeStopState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }

    private bool isTimeStopInput;
    private bState state;
    public override void DoCheck()
    {
        base.DoCheck();
        isTimeStopInput = player.inputHandler.isTimeStopEnable;
    }

    public override void Enter()
    {
        base.Enter();
        if(state == bState.air)
        {
            player.RB.gravityScale = 0;
        }

        player.SetVelocity(Vector3.zero);
        isTimeStopInput = player.inputHandler.isTimeStopEnable;
        if(isTimeStopInput)
        {
            SkillManager.instance.timeStop.CreatTimeStop(player.transform);
        }
    }

    public override void Exit()
    {
        base.Exit();
        if (state == bState.air)
        {
            player.RB.gravityScale = 3.5f;
        }
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if (!isTimeStopInput)
        {
            isAbilityDone = true;
            SkillManager.instance.timeStop.CancleSkill();
        }
    }

    public override void SetAdditionalData(object value)
    {
        if (value == null)
            return;
        state = (bState)value;
    }
}
