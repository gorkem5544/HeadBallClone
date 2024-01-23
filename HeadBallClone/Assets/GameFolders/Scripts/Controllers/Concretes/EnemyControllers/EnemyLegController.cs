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
    EnemyShoot _enemyShoot;
    [SerializeField] Transform _leg;
    Collider2D[] _attackResults;
    [SerializeField] EnemyScriptableObject _enemyScriptableObject;
    private void Awake()
    {
        _enemyShoot = new EnemyShoot(_leg, _enemyScriptableObject);
        _attackResults = new Collider2D[10];
        _rigidbody2D = GetComponentInParent<Rigidbody2D>();
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
