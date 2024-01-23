public class BallPhysicsStateMachine : BaseStateMachine
{
    private IState _currentState;
    BallStandardState _ballStandardState;
    BallConstantlyBounceState _ballConstantlyBounceState;
    BallLittleBounceState _ballLittleBounceState;


    public BallPhysicsStateMachine(BallController ballController)
    {
        _ballStandardState = new BallStandardState(ballController);
        _ballConstantlyBounceState = new BallConstantlyBounceState(ballController);
        _ballLittleBounceState = new BallLittleBounceState(ballController);
    }

    public BallLittleBounceState BallLittleBounceState { get => _ballLittleBounceState; set => _ballLittleBounceState = value; }
    public BallConstantlyBounceState BallConstantlyBounceState { get => _ballConstantlyBounceState; set => _ballConstantlyBounceState = value; }
    public BallStandardState BallStandardState { get => _ballStandardState; set => _ballStandardState = value; }


}
