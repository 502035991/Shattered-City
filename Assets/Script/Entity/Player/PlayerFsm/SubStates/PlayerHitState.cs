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
        if (!player.isControlled)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
