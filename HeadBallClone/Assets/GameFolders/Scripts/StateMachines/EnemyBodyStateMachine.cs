using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyBodyStateMachine
{
    public IState CurrentState { get; private set; }
    public EnemyControllerStandState enemyControllerStandState;
    public EnemyControllerMoveBallState enemyControllerMoveBallState;

    public EnemyBodyStateMachine(EnemyController enemyController)
    {
        enemyControllerStandState = new EnemyControllerStandState(enemyController);
        enemyControllerMoveBallState = new EnemyControllerMoveBallState(enemyController);
    }
    public void Initialize(IState state)
    {
        CurrentState = state;
        state.EnterState();

    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.ExitState();
        CurrentState = nextState;
        nextState.EnterState();
    }
    public void Update()
    {

        if (CurrentState != null)
        {
            CurrentState.UpdateState();
        }
    }
}
