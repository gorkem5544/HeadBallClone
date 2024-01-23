public class CastleStateMachine
{
    IState _currentState;
    private CastleDefaultState _castleDefaultState;
    private CastleHeighSizeState _castleHeighSizeState;
    private CastleSmallSizeState _castleSmallSizeState;
    private CastleFreezeState _castleFreezeState;
    public CastleStateMachine(CastleController porteController)
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
