using UnityEngine;

public class EnemyLegController : MonoBehaviour
{
    EnemyLegStateMachine _enemyLegStateMachine;
    [SerializeField] LayerMask _ballLayer;

    public LayerMask BallLayer { get => _ballLayer; set => _ballLayer = value; }
    public EnemyLegStateMachine EnemyLegStateMachine { get => _enemyLegStateMachine; set => _enemyLegStateMachine = value; }
    public PlayerShoot PlayerShoot { get => _playerShoot; set => _playerShoot = value; }

    PlayerShoot _playerShoot;
    [SerializeField] Transform _leg;
    private void Awake()
    {
        _playerShoot = new PlayerShoot(_leg);
        _enemyLegStateMachine = new EnemyLegStateMachine(this);
    }
    private void Start()
    {
        _enemyLegStateMachine.Initialize(_enemyLegStateMachine.enemyLegControllerNotShootState);
    }
    private void Update()
    {
        _enemyLegStateMachine.Update();
    }
}


public class EnemyLegStateMachine
{
    public IState CurrentState { get; private set; }
    public EnemyLegControllerShootState enemyLegControllerShootState;
    public EnemyLegControllerNotShootState enemyLegControllerNotShootState;

    public EnemyLegStateMachine(EnemyLegController enemyLegController)
    {
        enemyLegControllerShootState = new EnemyLegControllerShootState(enemyLegController);
        enemyLegControllerNotShootState = new EnemyLegControllerNotShootState(enemyLegController);
    }
    public void Initialize(IState state)
    {
        CurrentState = state;
        state.EnterState();

    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.ExitState();
        CurrentState = nextState;
        nextState.EnterState();
    }
    public void Update()
    {

        if (CurrentState != null)
        {
            CurrentState.UpdateState();
        }
    }
}
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
        _enemyLegController1.PlayerShoot.ResetLeg();

    }

    public void UpdateState()
    {
        Debug.Log("Shoot");
        timer += Time.deltaTime;
        _enemyLegController1.PlayerShoot.ShootLeg();
        if (timer > 1)
        {
            Debug.Log("asdasd");
            _enemyLegController1.PlayerShoot.ResetLeg();
            _enemyLegController1.EnemyLegStateMachine.TransitionTo(_enemyLegController1.EnemyLegStateMachine.enemyLegControllerNotShootState);
        }
    }
}
public class EnemyLegControllerNotShootState : IState
{
    EnemyLegController _enemyLegController1;
    public EnemyLegControllerNotShootState(EnemyLegController enemyLegController)
    {
        _enemyLegController1 = enemyLegController;
    }
    public void EnterState()
    {
        _enemyLegController1.PlayerShoot.ResetLeg();
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        Debug.Log("Not Shoot");
        Debug.DrawRay(_enemyLegController1.transform.position, -_enemyLegController1.transform.right, Color.blue, 1f);

        RaycastHit2D raycastHit2D = Physics2D.Raycast(_enemyLegController1.transform.position, -_enemyLegController1.transform.right, 1f, _enemyLegController1.BallLayer);
        if (raycastHit2D.collider != null)
        {
            _enemyLegController1.EnemyLegStateMachine.TransitionTo(_enemyLegController1.EnemyLegStateMachine.enemyLegControllerShootState);
        }
    }
}