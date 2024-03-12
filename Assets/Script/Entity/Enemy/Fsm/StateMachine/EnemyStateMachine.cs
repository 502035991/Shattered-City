using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState {  get; private set; }

    public void Initialize(EnemyState startState)
    {
        currentState = startState;
        currentState.Enter();
    }
    public void ChangeState(EnemyState newState , object value = null)
    {
        currentState.Exit();
        currentState = newState;
        currentState.SetAdditionalData(value);
        currentState.Enter();
    }
}
