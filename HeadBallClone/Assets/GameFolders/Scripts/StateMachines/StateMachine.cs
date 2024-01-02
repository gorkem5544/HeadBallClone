using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    void NormalStateTransition(IState from, IState to, System.Func<bool> condition);
    void anyStateTransition(IState to, System.Func<bool> condition);

    void SetState(IState state);
    void Update();
}
public class StateMachine : IStateMachine
{
    List<IStateTransition> _normalStateTransitions = new List<IStateTransition>();
    List<IStateTransition> _anyStateTransitions = new List<IStateTransition>();
    IState _currentState;

    public void anyStateTransition(IState to, Func<bool> condition)
    {
        _anyStateTransitions.Add(new StateTransition(null, to, condition));
    }

    public void NormalStateTransition(IState from, IState to, Func<bool> condition)
    {
        _anyStateTransitions.Add(new StateTransition(from, to, condition));
    }

    public void Update()
    {

        IStateTransition stateTransition = CheckForTransition();
        if (stateTransition != null)
        {
            SetState(stateTransition.To);
        }
        _currentState.UpdateState();
    }
    public void SetState(IState state)
    {
        _currentState?.ExitState();
        _currentState = state;
        _currentState.EnterState();
    }
    private IStateTransition CheckForTransition()
    {
        foreach (IStateTransition anyStateTransition in _anyStateTransitions)
        {
            if (anyStateTransition.Condition.Invoke())
            {
                return anyStateTransition;
            }
        }
        foreach (IStateTransition stateTransition in _normalStateTransitions)
        {
            if (stateTransition.Condition() && stateTransition.From == _currentState)
            {
                return stateTransition;
            }
        }
        return null;
    }
}

public interface IStateTransition
{
    IState To { get; }
    Func<bool> Condition { get; }
    IState From { get; }
}
public class StateTransition : IStateTransition
{
    IState _from;
    IState _to;
    System.Func<bool> _condition;

    public StateTransition(IState from, IState to, Func<bool> condition)
    {
        _to = to;
        _condition = condition;
        From = from;
    }

    public IState To { get => _to; set => _to = value; }
    public Func<bool> Condition { get => _condition; set => _condition = value; }
    public IState From { get => _from; set => _from = value; }
}

public interface IState
{
    void EnterState();
    void ExitState();
    void UpdateState();
}
