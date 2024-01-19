using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public System.Action<CastleTypeEnum> CharacterGoalEvent;
    GameManagerStateMachine _stateMachine;
    public GameManagerStateMachine StateMachine { get => _stateMachine; set => _stateMachine = value; }

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
        _stateMachine = new GameManagerStateMachine(this);
    }
    private void Start()
    {
        _stateMachine.SetState(_stateMachine.GameManagerGameState);

        CharacterGoalEvent += HandleOnPlayerGoal;
    }

    private void HandleOnPlayerGoal(CastleTypeEnum castleTypeEnum)
    {
        switch (castleTypeEnum)
        {
            case CastleTypeEnum.PlayerCastle:
                _stateMachine.StateMachineTransitionState(_stateMachine.GameManagerEnemyGoalState);
                break;
            case CastleTypeEnum.EnemyCastle:
                _stateMachine.StateMachineTransitionState(_stateMachine.GameManagerPlayerGoalState);
                break;
        }


    }
    private void OnDisable()
    {
        CharacterGoalEvent -= HandleOnPlayerGoal;
    }

    private void Update()
    {
        _stateMachine.UpdateState();
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
    public void UpdateState()
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

        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            _gameManager.StateMachine.StateMachineTransitionState(_gameManager.StateMachine.GameManagerGameState);
        }
    }

    //PlayerManager Sınıfına Taşıncak
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
            _gameManager.StateMachine.StateMachineTransitionState(_gameManager.StateMachine.GameManagerTimerState);
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
            _gameManager.StateMachine.StateMachineTransitionState(_gameManager.StateMachine.GameManagerTimerState);
        }
    }
}