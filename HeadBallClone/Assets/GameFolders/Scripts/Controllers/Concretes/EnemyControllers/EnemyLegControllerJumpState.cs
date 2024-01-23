using UnityEngine;

public class EnemyLegControllerJumpState : IState
{
    EnemyLegController _enemyLegController;
    public EnemyLegControllerJumpState(EnemyLegController enemyLegController)
    {
        _enemyLegController = enemyLegController;
    }
    public void EnterState()
    {
        _enemyLegController.EnemyLegJumpWithRigidbody2D.JumpTick();
    }
    public void ExitState()
    {
    }
    public void UpdateState()
    {
        Debug.Log("Jump State");
        _enemyLegController.EnemyLegStateMachine.TransitionTo(_enemyLegController.EnemyLegStateMachine.EnemyLegControllerFallState);
    }
}
