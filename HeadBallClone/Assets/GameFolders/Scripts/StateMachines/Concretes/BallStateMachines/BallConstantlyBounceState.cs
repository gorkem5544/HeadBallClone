using UnityEngine;

public class BallConstantlyBounceState : IState
{
    BallController _ballController;
    float _currentTime;
    public BallConstantlyBounceState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _ballController.Collider2D.sharedMaterial = _ballController.BallConstantlyBounce;
        _currentTime = 0;
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
