using UnityEngine;

public class EnemyControllerStandState : IState
{
    EnemyController _enemyController;
    float _timer;
    public bool IsStand { get; set; }
    public EnemyControllerStandState(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }
    public void EnterState()
    {
        _timer = 0;

        IsStand = false;
    }

    public void ExitState()
    {
        _timer = 0;
        IsStand = false;
    }

    public void UpdateState()
    {
        if (Vector2.Distance(_enemyController.transform.position, _enemyController.BallController.transform.position) < 5)
        {
            _enemyController.StateMachine.TransitionTo(_enemyController.StateMachine.enemyControllerMoveBallState);
        }

    }

}


