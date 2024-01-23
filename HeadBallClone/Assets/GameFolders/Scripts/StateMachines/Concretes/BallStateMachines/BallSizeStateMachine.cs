using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSizeStateMachine : BaseStateMachine
{
    private BallStandardSizeState _ballStandardSizeState;
    private BallHugeSizeState _ballHugeSizeState;
    private BallSmallSizeState _ballSmallSizeState;
    public BallSizeStateMachine(BallController ballController)
    {
        _ballStandardSizeState = new BallStandardSizeState(ballController);
        _ballHugeSizeState = new BallHugeSizeState(ballController);
        _ballSmallSizeState = new BallSmallSizeState(ballController);
    }
    public BallSmallSizeState BallSmallSizeState => _ballSmallSizeState;
    public BallHugeSizeState BallHugeSizeState => _ballHugeSizeState;
    public BallStandardSizeState BallStandardSizeState => _ballStandardSizeState;
}

public class BallStandardSizeState : IState
{
    BallController _ballController;
    public BallStandardSizeState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _ballController.UpdateSize(1f, 1f);
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        Debug.Log("Ball Standard Size State");
    }
}
public class BallHugeSizeState : IState
{
    BallController _ballController;
    float _currentTime;
    public BallHugeSizeState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _currentTime = 0;
        _ballController.UpdateSize(2f, 2f);
    }

    public void ExitState()
    {
        _currentTime = 0;

    }

    public void UpdateState()
    {
        Debug.Log("Ball Huge Size State");
        _currentTime += Time.deltaTime;
        if (_currentTime > 5)
        {
            _ballController.BallSizeStateMachine.StateMachineTransitionState(_ballController.BallSizeStateMachine.BallStandardSizeState);
        }

    }
}
public class BallSmallSizeState : IState
{
    BallController _ballController;
    float _currentTime;
    public BallSmallSizeState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _currentTime = 0;
        _ballController.UpdateSize(0.5f, 0.5f);

    }

    public void ExitState()
    {
        _currentTime = 0;

    }

    public void UpdateState()
    {
        Debug.Log("Ball Small Size State");
        _currentTime += Time.deltaTime;
        if (_currentTime > 5)
        {
            _ballController.BallSizeStateMachine.StateMachineTransitionState(_ballController.BallSizeStateMachine.BallStandardSizeState);
        }

    }
}