using UnityEngine;

public class CastleHeighSizeState : IState
{
    Vector2 _smallPowerSize;
    Vector2 _smallPowerTransform;
    float _currentTime;
    CastleController _porteController;
    public CastleHeighSizeState(CastleController porteController)
    {
        _porteController = porteController;
        _smallPowerSize = new Vector2(_porteController.transform.localScale.x, 2);
        _smallPowerTransform = new Vector2(_porteController.transform.position.x, -2.82f);
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
