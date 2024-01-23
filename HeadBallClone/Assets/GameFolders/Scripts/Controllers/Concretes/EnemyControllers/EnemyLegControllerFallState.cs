using UnityEngine;

public class EnemyLegControllerFallState : IState
{
    EnemyLegController _enemyLegController;
    public EnemyLegControllerFallState(EnemyLegController enemyLegController)
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
        CheckIfFootsOnGround(_enemyLegController.transform);
        Debug.Log("Fall Update");
    }
    private void CheckIfFootsOnGround(Transform footTransform)
    {
        RaycastHit2D hit = Physics2D.Raycast(footTransform.position, _enemyLegController.Forward.forward, 0.5f, _enemyLegController.Layers);
        Debug.DrawRay(footTransform.position, _enemyLegController.Forward.forward * 0.5f, Color.red);

        if (hit.collider != null)
        {
            _enemyLegController.EnemyLegStateMachine.TransitionTo(_enemyLegController.EnemyLegStateMachine.EnemyLegControllerGroundState);
        }
    }
}