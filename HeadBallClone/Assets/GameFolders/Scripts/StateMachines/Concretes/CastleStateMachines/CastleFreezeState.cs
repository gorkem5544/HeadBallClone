using UnityEngine;

public class CastleFreezeState : IState
{
    float _currentTime;
    CastleController _porteController;
    public CastleFreezeState(CastleController porteController)
    {
        _porteController = porteController;
    }
    public void EnterState()
    {
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
            _porteController.CastleStateMachine.StateMachineTransitionState(_porteController.CastleStateMachine.CastleDefaultState);
        }


    }
}