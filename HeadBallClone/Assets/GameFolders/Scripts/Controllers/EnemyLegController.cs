using UnityEngine;

public class EnemyLegController : MonoBehaviour
{
    EnemyLegStateMachine _enemyLegStateMachine;
    [SerializeField] LayerMask _ballLayer;
    BallController _ballController;
    EnemyLegJumpWithRigidbody2D _enemyLegJumpWithRigidbody2D;
    public LayerMask BallLayer { get => _ballLayer; set => _ballLayer = value; }
    public EnemyLegStateMachine EnemyLegStateMachine { get => _enemyLegStateMachine; set => _enemyLegStateMachine = value; }
    public PlayerShoot PlayerShoot { get => _playerShoot; set => _playerShoot = value; }
    public BallController BallController { get => _ballController; set => _ballController = value; }
    public Rigidbody2D Rigidbody2D { get => _rigidbody2D; set => _rigidbody2D = value; }
    public EnemyLegJumpWithRigidbody2D EnemyLegJumpWithRigidbody2D { get => _enemyLegJumpWithRigidbody2D; set => _enemyLegJumpWithRigidbody2D = value; }
    public LayerMask Layers { get => _layers; set => _layers = value; }
    public Transform Forward { get => _forward; set => _forward = value; }
    public Collider2D[] AttackResults { get => _attackResults; set => _attackResults = value; }

    [SerializeField] Transform _forward;

    [SerializeField] LayerMask _layers;
    Rigidbody2D _rigidbody2D;

    PlayerShoot _playerShoot;
    [SerializeField] Transform _leg;
    Collider2D[] _attackResults;
    private void Awake()
    {
        _attackResults = new Collider2D[10];
        _rigidbody2D = GetComponentInParent<Rigidbody2D>();
        _playerShoot = new PlayerShoot(_leg);
        _ballController = GameObject.FindGameObjectWithTag("Ball").transform.GetComponent<BallController>();
        _enemyLegJumpWithRigidbody2D = new EnemyLegJumpWithRigidbody2D(this);
        _enemyLegStateMachine = new EnemyLegStateMachine(this);
    }
    private void Start()
    {
        _enemyLegStateMachine.Initialize(_enemyLegStateMachine.EnemyLegControllerGroundState);
    }
    private void Update()
    {
        _enemyLegStateMachine.Update();
    }
    private void OnDrawGizmos()
    {
        OnDrawGizmosSelected();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + 0.75f), 0.75f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + 0.75f), 0.5f);
    }
}


public class EnemyLegStateMachine
{
    public IState CurrentState { get; private set; }
    public EnemyLegControllerJumpState EnemyLegControllerJumpState { get => _enemyLegControllerJumpState; set => _enemyLegControllerJumpState = value; }
    public EnemyLegControllerGroundState EnemyLegControllerGroundState { get => _enemyLegControllerGroundState; set => _enemyLegControllerGroundState = value; }
    public EnemyLegControllerFallState EnemyLegControllerFallState { get => _enemyLegControllerFallState; set => _enemyLegControllerFallState = value; }

    public EnemyLegControllerShootState enemyLegControllerShootState;
    public EnemyLegControllerNotShootState enemyLegControllerNotShootState;
    private EnemyLegControllerJumpState _enemyLegControllerJumpState;
    private EnemyLegControllerGroundState _enemyLegControllerGroundState;
    private EnemyLegControllerFallState _enemyLegControllerFallState;

    public EnemyLegStateMachine(EnemyLegController enemyLegController)
    {
        enemyLegControllerShootState = new EnemyLegControllerShootState(enemyLegController);
        enemyLegControllerNotShootState = new EnemyLegControllerNotShootState(enemyLegController);
        _enemyLegControllerJumpState = new EnemyLegControllerJumpState(enemyLegController);
        _enemyLegControllerGroundState = new EnemyLegControllerGroundState(enemyLegController);
        _enemyLegControllerFallState = new EnemyLegControllerFallState(enemyLegController);
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

        Debug.DrawRay(_enemyLegController1.transform.position, -_enemyLegController1.transform.right, Color.blue, 1f);

        RaycastHit2D raycastHit2D = Physics2D.Raycast(_enemyLegController1.transform.position, -_enemyLegController1.transform.right, 1f, _enemyLegController1.BallLayer);
        if (raycastHit2D.collider != null)
        {
            _enemyLegController1.EnemyLegStateMachine.TransitionTo(_enemyLegController1.EnemyLegStateMachine.enemyLegControllerShootState);
        }
    }
}






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
            Debug.Log(takeHit);
            if (takeHit != null)
            {
                _enemyLegController.EnemyLegStateMachine.TransitionTo(_enemyLegController.EnemyLegStateMachine.EnemyLegControllerJumpState);
            }
        }
        Debug.Log("Ground State");
    }
    private float DistanceMeToBall()
    {
        return Vector2.Distance(_enemyLegController.transform.position, _enemyLegController.BallController.transform.position);
    }

}


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
public class EnemyLegJumpWithRigidbody2D
{
    EnemyLegController _enemyLegController;
    public EnemyLegJumpWithRigidbody2D(EnemyLegController enemyLegController)
    {
        _enemyLegController = enemyLegController;
    }
    public void JumpTick()
    {
        _enemyLegController.Rigidbody2D.AddForce(Vector2.up * 300f, ForceMode2D.Force);
    }
}

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