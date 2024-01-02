using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyAIStateEnum
{
    Stand, Jump, Run,
}
public enum EnemyAICombatState
{
    Shoot, NotShoot
}
public class EnemyController : MonoBehaviour, IEnemyVerticalMoveWithTransform
{
    EnemyAIStateEnum _enemyAIStateEnum;
    [SerializeField] Transform _groundCheckerTransform;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _distance;
    IOnGround _onGround;

    Rigidbody2D _rigidbody2D;
    BallController _ballController;


    public IEntityController TargetController => _ballController;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;



    IEnemyVerticalMove _enemyVerticalMove;
    Collider2D[] _attackResults;
    IStateMachine _stateMachine;
    private void Awake()
    {
        _stateMachine = new StateMachine();
        _enemyVerticalMove = new EnemyVerticalMoveWithTransform(this);
        _ballController = GameObject.FindGameObjectWithTag("Ball").transform.GetComponent<BallController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _onGround = new OnGround(_groundCheckerTransform, _distance, _groundLayer);
    }
    private void Start()
    {
        EnemyControllerStandState enemyControllerStandState = new EnemyControllerStandState();
        EnemyControllerMoveBallState enemyControllerMoveBallState = new EnemyControllerMoveBallState();
        _stateMachine.SetState(enemyControllerStandState);

        // _stateMachine.NormalStateTransition(enemyControllerStandState, enemyControllerMoveBallState, () => CanShoot());

    }
    private void Update()
    {
        _stateMachine.Update();
    }
    private void FixedUpdate()
    {
        _enemyVerticalMove.VerticalMoveFixedUpdate();
    }
    public void ChangeAI(EnemyAIStateEnum enemyAIStateEnum)
    {
        _enemyAIStateEnum = enemyAIStateEnum;
    }

    private void OnDrawGizmos()
    {
        OnDrawGizmosSelected();
    }
    private void OnDrawGizmosSelected()
    {
        Color color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, 0.5f);

    }
    private void CanShoot()
    {
        // int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, 0.25f, _attackResults);
        // for (int i = 0; i < hitCount; i++)
        // {
        //     BallController ballController = _attackResults[i].GetComponent<BallController>();
        //     if (ballController != null)
        //     {
        //        ChangeAI(_enemyAIStateEnum.)
        //     }
        // }


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
    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        Debug.Log("Stand Update");

    }
}
public class EnemyControllerMoveBallState : IState
{
    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        Debug.Log("Move Update");

    }
}
public class EnemyControllerMoveKaleState
{
}
public class EnemyControllerJumpState
{
}


public class EnemyLegController
{

}
public class EnemyLegControllerShootState
{
}
public class EnemyLegControllerJumpShootState
{
}
public class EnemyLegControllerNotShootState
{
}