public class BallStandardState : IState
{
    BallController _ballController;
    public BallStandardState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _ballController.Collider2D.sharedMaterial = _ballController.BallStandardBounce;
        _ballController.UpdateSize(1, 1);
    }

    public void ExitState()
    {
    }

    public void UpdateState()
    {
    }
}
