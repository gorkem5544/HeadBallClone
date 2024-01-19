using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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
        _onGround = new OnGround(_groundCheckerTransform, _distance, _groundLayer);
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




public interface IEnemyVerticalMove : IAbstractEntityVerticalMove
{

}
public interface IEnemyVerticalMoveWithTransform : IEntityController
{
    IEntityController TargetController { get; }
}
public class EnemyVerticalMoveWithTransform : IEnemyVerticalMove
{
    IEnemyVerticalMoveWithTransform _enemyVerticalMoveWithTransform;
    Transform _targetTransform => _enemyVerticalMoveWithTransform.TargetController.transform;
    public EnemyVerticalMoveWithTransform(IEnemyVerticalMoveWithTransform enemyVerticalMoveWithTransform)
    {
        _enemyVerticalMoveWithTransform = enemyVerticalMoveWithTransform;
    }
    public void VerticalMoveFixedUpdate()
    {
        _enemyVerticalMoveWithTransform.transform.position = Vector2.MoveTowards(_enemyVerticalMoveWithTransform.transform.position, new Vector2(_targetTransform.position.x, _enemyVerticalMoveWithTransform.transform.position.y), 3 * Time.deltaTime);
    }
}






















public class EnemyControllerStandState : IState
{
    EnemyController _enemyController;
    float _timer;
    public bool IsStand { get; set; }
    public EnemyControllerStandState(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }
    public void EnterState()
    {
        _timer = 0;

        IsStand = false;
    }

    public void ExitState()
    {
        _timer = 0;
        IsStand = false;
    }

    public void UpdateState()
    {
        if (Vector2.Distance(_enemyController.transform.position, _enemyController.BallController.transform.position) < 5)
        {
            _enemyController.StateMachine.TransitionTo(_enemyController.StateMachine.enemyControllerMoveBallState);
        }

    }

}
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
        _enemyVerticalMoveWithTransform.VerticalMoveFixedUpdate();
    }
}
public class EnemyControllerMoveKaleState
{
}
public class EnemyControllerJumpState
{
}
public class EnemyLegControllerJumpShootState
{
}


