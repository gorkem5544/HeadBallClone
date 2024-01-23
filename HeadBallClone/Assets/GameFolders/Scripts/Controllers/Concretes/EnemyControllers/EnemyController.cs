using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour, IEnemyVerticalMoveWithTransform
{

    [SerializeField] Transform _groundCheckerTransform;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _distance;
    IOnGround _onGround;

    Rigidbody2D _rigidbody2D;
    BallController _ballController;


    public IEntityController TargetController => _ballController;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    public BallController BallController { get => _ballController; set => _ballController = value; }
    public IEnemyVerticalMove EnemyVerticalMove { get => _enemyVerticalMove; set => _enemyVerticalMove = value; }

    IEnemyVerticalMove _enemyVerticalMove;
    Collider2D[] _attackResults;
    EnemyBodyStateMachine _stateMachine;
    public EnemyBodyStateMachine StateMachine { get => _stateMachine; set => _stateMachine = value; }
    private void Awake()
    {
        _enemyVerticalMove = new EnemyVerticalMoveWithTransform(this);
        _ballController = GameObject.FindGameObjectWithTag("Ball").transform.GetComponent<BallController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //_onGround = new GroundDetector(_groundCheckerTransform, _distance, _groundLayer);
        _stateMachine = new EnemyBodyStateMachine(this);
    }
    private void Start()
    {
        _stateMachine.Initialize(_stateMachine.enemyControllerStandState);
    }
    private void Update()
    {
        _stateMachine.Update();
    }
}


