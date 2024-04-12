using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerAbilityState
{
    public PlayerHitState(PlayerStateMachine stateMachine, PlayerData playerData, Player player, string animName) : base(stateMachine, playerData, player, animName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();
        if (!player.GetControlState())
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
