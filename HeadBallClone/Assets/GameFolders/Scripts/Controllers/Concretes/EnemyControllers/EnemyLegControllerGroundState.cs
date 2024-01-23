using UnityEngine;

public class EnemyLegControllerGroundState : IState
{
    EnemyLegController _enemyLegController;
    private float _jumpDistance = 1.5f;

    public EnemyLegControllerGroundState(EnemyLegController enemyLegController)
    {
        _enemyLegController = enemyLegController;
    }
    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {

        int hitCount = Physics2D.OverlapCircleNonAlloc(new Vector2(_enemyLegController.transform.position.x, _enemyLegController.transform.position.y + 0.75f), 0.5f, _enemyLegController.AttackResults);
        Debug.DrawRay(_enemyLegController.transform.position, new Vector2(_enemyLegController.transform.position.x, _enemyLegController.transform.position.y + 0.75f), Color.green);
        Debug.DrawLine(_enemyLegController.transform.position, new Vector2(_enemyLegController.transform.position.x, _enemyLegController.transform.position.y + 0.75f), Color.green);

        for (int i = 0; i < hitCount; i++)
        {
            BallController takeHit = _enemyLegController.AttackResults[i].GetComponent<BallController>();

            if (takeHit != null)
            {
                _enemyLegController.EnemyLegStateMachine.TransitionTo(_enemyLegController.EnemyLegStateMachine.EnemyLegControllerJumpState);
            }
        }

    }
    private float DistanceMeToBall()
    {
        return Vector2.Distance(_enemyLegController.transform.position, _enemyLegController.BallController.transform.position);
    }

}
