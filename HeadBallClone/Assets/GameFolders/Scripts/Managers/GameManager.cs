using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public System.Action<CastleTypeEnum> PlayerGoalEvent;
    GameManagerStateMachine _gameManagerStateMachine;

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
    public GameManagerStateMachine(GameManager gameManager)
    {
        _gameManagerGameState = new GameManagerGameState(gameManager);
        _gameManagerPlayerGoalState = new GameManagerPlayerGoalState(gameManager);
        _gameManagerEnemyGoalState = new GameManagerEnemyGoalState(gameManager);
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
public class GameManagerPlayerGoalState : IState
{
    public GameManagerPlayerGoalState(GameManager gameManager)
    {

    }
    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        Debug.Log("Player Goal");

    }
}
public class GameManagerEnemyGoalState : IState
{
    public GameManagerEnemyGoalState(GameManager gameManager)
    {

    }
    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        Debug.Log("Enemy Goal");
    }
}