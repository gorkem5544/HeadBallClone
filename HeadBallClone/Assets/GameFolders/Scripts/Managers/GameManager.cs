using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public System.Action<CastleTypeEnum> PlayerGoalEvent;
    GameManagerStateMachine _gameManagerStateMachine;
    public GameManagerStateMachine GameManagerStateMachine { get => _gameManagerStateMachine; set => _gameManagerStateMachine = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        _gameManagerStateMachine = new GameManagerStateMachine(this);

    }
    private void Start()
    {
        _gameManagerStateMachine.SetState(_gameManagerStateMachine.GameManagerGameState);

        PlayerGoalEvent += HandleOnPlayerGoal;
    }

    private void HandleOnPlayerGoal(CastleTypeEnum castleTypeEnum)
    {
        switch (castleTypeEnum)
        {
            case CastleTypeEnum.PlayerCastle:
                _gameManagerStateMachine.StateMachineTransitionState(_gameManagerStateMachine.GameManagerEnemyGoalState);
                break;
            case CastleTypeEnum.EnemyCastle:
                _gameManagerStateMachine.StateMachineTransitionState(_gameManagerStateMachine.GameManagerPlayerGoalState);
                break;
        }


    }
    private void OnDisable()
    {
        PlayerGoalEvent -= HandleOnPlayerGoal;

    }

    private void Update()
    {
        _gameManagerStateMachine.StateMachineUpdateTick();
    }
}

#region StateMachine
public class GameManagerStateMachine
{

    private IState _currentState;

    private GameManagerGameState _gameManagerGameState;
    public GameManagerGameState GameManagerGameState { get => _gameManagerGameState; set => _gameManagerGameState = value; }

    private GameManagerPlayerGoalState _gameManagerPlayerGoalState;
    public GameManagerPlayerGoalState GameManagerPlayerGoalState { get => _gameManagerPlayerGoalState; set => _gameManagerPlayerGoalState = value; }

    private GameManagerEnemyGoalState _gameManagerEnemyGoalState;
    public GameManagerEnemyGoalState GameManagerEnemyGoalState { get => _gameManagerEnemyGoalState; set => _gameManagerEnemyGoalState = value; }

    private GameManagerTimerState _gameManagerTimerState;
    public GameManagerTimerState GameManagerTimerState { get => _gameManagerTimerState; set => _gameManagerTimerState = value; }
    public GameManagerStateMachine(GameManager gameManager)
    {
        _gameManagerGameState = new GameManagerGameState(gameManager);
        _gameManagerPlayerGoalState = new GameManagerPlayerGoalState(gameManager);
        _gameManagerEnemyGoalState = new GameManagerEnemyGoalState(gameManager);
        _gameManagerTimerState = new GameManagerTimerState(gameManager);
    }
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
#endregion


public class GameManagerGameState : IState
{
    GameManager _gameManager1;
    public GameManagerGameState(GameManager gameManager)
    {
        _gameManager1 = gameManager;
    }
    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        Debug.Log("Game In");
    }
}
public class GameManagerTimerState : IState
{
    GameManager _gameManager;
    private float _currentTime = 0;
    public GameManagerTimerState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    public void EnterState()
    {
        _currentTime = 3;
        PlayerManager.Instance.ResetPlayerPosition();
        ChangedPlayerBodyType(RigidbodyType2D.Static);
    }

    public void ExitState()
    {
        _currentTime = 3;
        ChangedPlayerBodyType(RigidbodyType2D.Dynamic);

    }

    public void UpdateState()
    {
        Debug.Log("time In");
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            _gameManager.GameManagerStateMachine.StateMachineTransitionState(_gameManager.GameManagerStateMachine.GameManagerGameState);
        }
    }

    private void ChangedPlayerBodyType(RigidbodyType2D rigidbodyType2D)
    {
        PlayerManager.Instance.ChangedPlayerBodyType(rigidbodyType2D);
    }
}
public class GameManagerPlayerGoalState : IState
{
    private float _currentTime = 0;
    GameManager _gameManager;
    public GameManagerPlayerGoalState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    public void EnterState()
    {
        _currentTime = 3;
    }

    public void ExitState()
    {
        _currentTime = 3;
    }

    public void UpdateState()
    {
        Debug.Log("Player Goal");
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            _gameManager.GameManagerStateMachine.StateMachineTransitionState(_gameManager.GameManagerStateMachine.GameManagerTimerState);
        }
    }
}
public class GameManagerEnemyGoalState : IState
{
    private float _currentTime = 0;
    private GameManager _gameManager;
    public GameManagerEnemyGoalState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    public void EnterState()
    {
        _currentTime = 3;
    }

    public void ExitState()
    {
        _currentTime = 3;
    }

    public void UpdateState()
    {
        Debug.Log("Enemy Goal");
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            _gameManager.GameManagerStateMachine.StateMachineTransitionState(_gameManager.GameManagerStateMachine.GameManagerTimerState);
        }
    }
}