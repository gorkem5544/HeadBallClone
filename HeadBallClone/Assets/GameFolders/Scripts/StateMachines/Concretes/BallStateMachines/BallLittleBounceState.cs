using UnityEngine;

public class BallLittleBounceState : IState
{
    float _currentTime;
    BallController _ballController;
    public BallLittleBounceState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _currentTime = 0;
        _ballController.Collider2D.sharedMaterial = _ballController.BallLittleBounce;
    }

    public void ExitState()
    {
        _currentTime = 0;
    }

    public void UpdateState()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > 5)
        {
            _ballController.BallControllerStateMachine.StateMachineTransitionState(_ballController.BallControllerStateMachine.BallStandardState);
        }
    }
}
