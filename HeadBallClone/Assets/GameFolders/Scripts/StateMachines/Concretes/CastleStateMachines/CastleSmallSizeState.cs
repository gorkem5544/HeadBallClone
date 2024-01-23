using UnityEngine;

public class CastleSmallSizeState : IState
{
    CastleController _porteController;
    float _currentTime;
    Vector2 _smallPowerSize;
    Vector2 _smallPowerTransform;
    public CastleSmallSizeState(CastleController porteController)
    {
        _porteController = porteController;
        _smallPowerSize = new Vector2(_porteController.transform.localScale.x, 0.5f);
        _smallPowerTransform = new Vector2(_porteController.transform.position.x, -4f);
    }
    public void EnterState()
    {
        _currentTime = 0;
        _porteController.transform.position = _smallPowerTransform;
        _porteController.transform.localScale = _smallPowerSize;
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
