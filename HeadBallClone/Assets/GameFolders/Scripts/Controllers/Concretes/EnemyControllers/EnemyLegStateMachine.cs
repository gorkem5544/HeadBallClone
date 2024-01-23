public class EnemyLegStateMachine
{
    public IState CurrentState { get; private set; }
    public EnemyLegControllerJumpState EnemyLegControllerJumpState { get => _enemyLegControllerJumpState; set => _enemyLegControllerJumpState = value; }
    public EnemyLegControllerGroundState EnemyLegControllerGroundState { get => _enemyLegControllerGroundState; set => _enemyLegControllerGroundState = value; }
    public EnemyLegControllerFallState EnemyLegControllerFallState { get => _enemyLegControllerFallState; set => _enemyLegControllerFallState = value; }

    public EnemyLegControllerShootState enemyLegControllerShootState;
    public EnemyLegControllerNotShootState enemyLegControllerNotShootState;
    private EnemyLegControllerJumpState _enemyLegControllerJumpState;
    private EnemyLegControllerGroundState _enemyLegControllerGroundState;
    private EnemyLegControllerFallState _enemyLegControllerFallState;

    public EnemyLegStateMachine(EnemyLegController enemyLegController)
    {
        enemyLegControllerShootState = new EnemyLegControllerShootState(enemyLegController);
        enemyLegControllerNotShootState = new EnemyLegControllerNotShootState(enemyLegController);
        _enemyLegControllerJumpState = new EnemyLegControllerJumpState(enemyLegController);
        _enemyLegControllerGroundState = new EnemyLegControllerGroundState(enemyLegController);
        _enemyLegControllerFallState = new EnemyLegControllerFallState(enemyLegController);
    }
    public void Initialize(IState state)
    {
        CurrentState = state;
        state.EnterState();

    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.ExitState();
        CurrentState = nextState;
        nextState.EnterState();
    }
    public void Update()
    {

        if (CurrentState != null)
        {
            CurrentState.UpdateState();
        }
    }
}
