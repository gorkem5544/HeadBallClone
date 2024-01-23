using UnityEngine;

public class EnemyLegControllerNotShootState : IState
{
    EnemyLegController _enemyLegController1;
    public EnemyLegControllerNotShootState(EnemyLegController enemyLegController)
    {
        _enemyLegController1 = enemyLegController;
    }
    public void EnterState()
    {
        //_enemyLegController1.PlayerShoot.ResetAction();
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {

        Debug.DrawRay(_enemyLegController1.transform.position, -_enemyLegController1.transform.right, Color.blue, 1f);

        RaycastHit2D raycastHit2D = Physics2D.Raycast(_enemyLegController1.transform.position, -_enemyLegController1.transform.right, 1f, _enemyLegController1.BallLayer);
        if (raycastHit2D.collider != null)
        {
            _enemyLegController1.EnemyLegStateMachine.TransitionTo(_enemyLegController1.EnemyLegStateMachine.enemyLegControllerShootState);
        }
    }
}
