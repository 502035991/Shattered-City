using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState {  get; private set; }

    public void Initialize(PlayerState currentState)
    {
        this.currentState = currentState;
        currentState.Enter();
    }
    public void ChangeState(PlayerState newState,object value = null)
    {
        currentState.Exit();
        currentState = newState;
        currentState.SetAdditionalData(value);
        currentState.Enter();
    }
}
