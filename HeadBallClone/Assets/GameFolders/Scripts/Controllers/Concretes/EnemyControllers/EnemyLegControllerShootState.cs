using UnityEngine;

public class EnemyLegControllerShootState : IState
{
    EnemyLegController _enemyLegController1;
    float timer = 0;
    public EnemyLegControllerShootState(EnemyLegController enemyLegController)
    {
        _enemyLegController1 = enemyLegController;

    }
    public void EnterState()
    {
        timer = 0;

    }

    public void ExitState()
    {
        //_enemyLegController1.PlayerShoot.();

    }

    public void UpdateState()
    {

        timer += Time.deltaTime;
        //_enemyLegController1.PlayerShoot.ShotAction();
        if (timer > 1)
        {
            Debug.Log("asdasd");
            //  _enemyLegController1.PlayerShoot.ResetAction();
            _enemyLegController1.EnemyLegStateMachine.TransitionTo(_enemyLegController1.EnemyLegStateMachine.enemyLegControllerNotShootState);
        }
    }
}
