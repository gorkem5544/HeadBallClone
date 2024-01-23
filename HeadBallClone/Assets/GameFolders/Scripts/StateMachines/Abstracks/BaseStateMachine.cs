using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine
{
    IState _currentState;

    public virtual void SetState(IState state)
    {
        _currentState = state;
        _currentState.EnterState();
    }
    public virtual void StateMachineTransitionState(IState to)
    {
        _currentState?.ExitState();
        _currentState = to;
        _currentState?.EnterState();
    }
    public virtual void StateMachineUpdateTick()
    {
        _currentState?.UpdateState();
    }
}
