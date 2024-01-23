using UnityEngine;

public class EnemyControllerMoveBallState : IState
{
    IEnemyVerticalMove _enemyVerticalMoveWithTransform;
    EnemyController _enemyController;
    public EnemyControllerMoveBallState(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _enemyVerticalMoveWithTransform = _enemyController.EnemyVerticalMove;
    }
    public void EnterState()
    {
        Debug.Log(_enemyVerticalMoveWithTransform);
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        Debug.Log("Move Update");
        if (Vector2.Distance(_enemyController.transform.position, _enemyController.BallController.transform.position) > 5)
        {

        }
        //        _enemyVerticalMoveWithTransform.VerticalMoveFixedUpdate();
    }
}


