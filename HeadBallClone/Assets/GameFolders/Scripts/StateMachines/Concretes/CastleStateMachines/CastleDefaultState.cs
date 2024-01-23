using UnityEngine;

public class CastleDefaultState : IState
{
    CastleController _porteController;
    Vector2 _smallPowerSize;
    Vector2 _smallPowerTransform;
    public CastleDefaultState(CastleController porteController)
    {
        _porteController = porteController;
        _smallPowerSize = new Vector2(_porteController.transform.localScale.x, 1);
        _smallPowerTransform = new Vector2(_porteController.transform.position.x, -3.6f);
    }
    public void EnterState()
    {
        _porteController.transform.position = _smallPowerTransform;
        _porteController.transform.localScale = _smallPowerSize;
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {


    }
}
