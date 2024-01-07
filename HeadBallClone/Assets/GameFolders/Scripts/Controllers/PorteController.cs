using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CastleTypeEnum
{
    PlayerCastle, EnemyCastle
}
public class PorteController : MonoBehaviour
{
    [SerializeField] CastleTypeEnum _castleTypeEnum;
    CastleStateMachine _castleStateMachine;

    public CastleStateMachine CastleStateMachine { get => _castleStateMachine; set => _castleStateMachine = value; }

    private void Awake()
    {
        _castleStateMachine = new CastleStateMachine(this);
    }
    private void Start()
    {
        _castleStateMachine.SetState(_castleStateMachine.CastleDefaultState);
    }
    public void SmallSizePower()
    {
        _castleStateMachine.StateMachineTransitionState(_castleStateMachine.CastleSmallSizeState);
    }
    public void HeightSizePower()
    {
        _castleStateMachine.StateMachineTransitionState(_castleStateMachine.CastleHeighSizeState);
    }
    public void FreezePower()
    {
        _castleStateMachine.StateMachineTransitionState(_castleStateMachine.CastleFreezeState);
    }
    private void Update()
    {
        _castleStateMachine.StateMachineUpdateTick();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            GameManager.Instance.PlayerGoalEvent?.Invoke(_castleTypeEnum);
        }
    }
}


public class CastleStateMachine
{
    IState _currentState;
    private CastleDefaultState _castleDefaultState;
    private CastleHeighSizeState _castleHeighSizeState;
    private CastleSmallSizeState _castleSmallSizeState;
    private CastleFreezeState _castleFreezeState;
    public CastleStateMachine(PorteController porteController)
    {
        _castleDefaultState = new CastleDefaultState(porteController);
        _castleHeighSizeState = new CastleHeighSizeState(porteController);
        _castleSmallSizeState = new CastleSmallSizeState(porteController);
        _castleFreezeState = new CastleFreezeState(porteController);

    }

    public CastleHeighSizeState CastleHeighSizeState { get => _castleHeighSizeState; set => _castleHeighSizeState = value; }
    public CastleFreezeState CastleFreezeState { get => _castleFreezeState; set => _castleFreezeState = value; }
    public CastleSmallSizeState CastleSmallSizeState { get => _castleSmallSizeState; set => _castleSmallSizeState = value; }
    public CastleDefaultState CastleDefaultState { get => _castleDefaultState; set => _castleDefaultState = value; }

    public void SetState(IState state)
    {
        _currentState = state;
        _currentState.EnterState();
    }
    public void StateMachineTransitionState(IState to)
    {
        _currentState?.ExitState();
        _currentState = to;
        _currentState?.EnterState();
    }
    public void StateMachineUpdateTick()
    {
        _currentState?.UpdateState();
    }
}


public class CastleDefaultState : IState
{
    PorteController _porteController;
    Vector2 _smallPowerSize;
    Vector2 _smallPowerTransform;
    public CastleDefaultState(PorteController porteController)
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

        Debug.Log("Default" + ">> \b" + _porteController.gameObject.name);
    }
}
public class CastleSmallSizeState : IState
{
    PorteController _porteController;
    float _currentTime;
    Vector2 _smallPowerSize;
    Vector2 _smallPowerTransform;
    public CastleSmallSizeState(PorteController porteController)
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
        Debug.Log("Small" + ">> \b" + _porteController.gameObject.name);

    }
}
public class CastleHeighSizeState : IState
{
    Vector2 _smallPowerSize;
    Vector2 _smallPowerTransform;
    float _currentTime;
    PorteController _porteController;
    public CastleHeighSizeState(PorteController porteController)
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
        Debug.Log("Height" + ">> \b" + _porteController.gameObject.name);

    }

}
public class CastleFreezeState : IState
{
    float _currentTime;
    PorteController _porteController;
    public CastleFreezeState(PorteController porteController)
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
        Debug.Log("Freeze" + ">> \b" + _porteController.gameObject.name);

    }
}