using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallController : MonoBehaviour, IEntityController
{

    Rigidbody2D _rigidbody2D;
    Collider2D _collider2D;
    BallControllerStateMachine _ballControllerStateMachine;
    IEntityController _lastShootCharacter;
    IEntityController _rival;
    [SerializeField] PhysicsMaterial2D _ballLittleBounce;
    [SerializeField] PhysicsMaterial2D _ballStandardBounce;
    [SerializeField] PhysicsMaterial2D _ballConstantlyBounce;


    IEntityController _enemyController;
    IEntityController _playerController;
    public BallControllerStateMachine BallControllerStateMachine { get => _ballControllerStateMachine; set => _ballControllerStateMachine = value; }
    public IEntityController LastShootCharacter { get => _lastShootCharacter; set => _lastShootCharacter = value; }
    public IEntityController Rival { get => _rival; set => _rival = value; }
    public PhysicsMaterial2D BallLittleBounce { get => _ballLittleBounce; set => _ballLittleBounce = value; }
    public PhysicsMaterial2D BallConstantlyBounce { get => _ballConstantlyBounce; set => _ballConstantlyBounce = value; }
    public PhysicsMaterial2D BallStandardBounce { get => _ballStandardBounce; set => _ballStandardBounce = value; }
    public Rigidbody2D Rigidbody2D { get => _rigidbody2D; set => _rigidbody2D = value; }
    public Collider2D Collider2D { get => _collider2D; set => _collider2D = value; }

    private void Start()
    {
        _ballControllerStateMachine.SetState(_ballControllerStateMachine.BallStandardState);

        PowerManager.Instance.BallLittleBouncingPowerEvent += BallLittleBouncingPowerEvent;
        PowerManager.Instance.BallConstantlyBouncingPowerEvent += BallConstantlyBounceEvent;
        PowerManager.Instance.BallShrinkagePowerEvent += BallShrinkagePowerEvent;
        PowerManager.Instance.BallExpansionPowerEvent += BallExpansionPowerEvent;
    }
    private void Update()
    {
        _ballControllerStateMachine.StateMachineUpdateTick();
    }
    private void BallExpansionPowerEvent()
    {
        _ballControllerStateMachine.StateMachineTransitionState(_ballControllerStateMachine.BallExpansionState);
    }

    private void BallShrinkagePowerEvent()
    {
        _ballControllerStateMachine.StateMachineTransitionState(_ballControllerStateMachine.BallShrinkageState);
    }

    private void BallConstantlyBounceEvent()
    {
        _ballControllerStateMachine.StateMachineTransitionState(_ballControllerStateMachine.BallConstantlyBounceState);
    }

    private void OnDisable()
    {
        PowerManager.Instance.BallLittleBouncingPowerEvent -= BallLittleBouncingPowerEvent;
        PowerManager.Instance.BallConstantlyBouncingPowerEvent -= BallConstantlyBounceEvent;
        PowerManager.Instance.BallShrinkagePowerEvent -= BallShrinkagePowerEvent;
        PowerManager.Instance.BallExpansionPowerEvent -= BallExpansionPowerEvent;
    }

    private void BallLittleBouncingPowerEvent()
    {
        _ballControllerStateMachine.StateMachineTransitionState(_ballControllerStateMachine.BallLittleBounceState);
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _enemyController = GameObject.FindGameObjectWithTag("Enemy").transform.GetComponent<EnemyController>();
        _playerController = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>();
        _ballControllerStateMachine = new BallControllerStateMachine(this);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController playerController = other.collider.GetComponent<PlayerController>();
        EnemyController enemyController = other.collider.GetComponent<EnemyController>();

        if (playerController != null)
        {
            ContactPoint2D contact = other.contacts[0];
            Vector3 pos = contact.point;
            _rigidbody2D.AddForce(pos * 2);

            _lastShootCharacter = _playerController;
            _rival = _enemyController;
        }
        else if (enemyController != null)
        {
            _lastShootCharacter = _enemyController;
            _rival = _playerController;
        }

        if (other.collider.TryGetComponent(out FootController leg))
        {
            Vector2 yon = other.contacts[0].point - (Vector2)transform.position;
            yon = yon.normalized;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Abs(yon.x), Mathf.Abs(yon.y)) * 5, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        }
    }

    public void ChangeBallSize(float sizeX, float sizeY)
    {
        transform.localScale = new Vector2(sizeX, sizeY);
    }
    public void ChangeBallSpeed(float speed)
    {

    }

}


public class BallControllerStateMachine
{
    private IState _currentState;
    BallStandardState _ballStandardState;
    BallConstantlyBounceState _ballConstantlyBounceState;
    BallLittleBounceState _ballLittleBounceState;
    BallShrinkageState _ballShrinkageState;
    BallExpansionState _ballExpansionState;

    public BallControllerStateMachine(BallController ballController)
    {
        _ballStandardState = new BallStandardState(ballController);
        _ballConstantlyBounceState = new BallConstantlyBounceState(ballController);
        _ballLittleBounceState = new BallLittleBounceState(ballController);
        _ballShrinkageState = new BallShrinkageState(ballController);
        _ballExpansionState = new BallExpansionState(ballController);
    }

    public BallLittleBounceState BallLittleBounceState { get => _ballLittleBounceState; set => _ballLittleBounceState = value; }
    public BallConstantlyBounceState BallConstantlyBounceState { get => _ballConstantlyBounceState; set => _ballConstantlyBounceState = value; }
    public BallStandardState BallStandardState { get => _ballStandardState; set => _ballStandardState = value; }
    public BallShrinkageState BallShrinkageState { get => _ballShrinkageState; set => _ballShrinkageState = value; }
    public BallExpansionState BallExpansionState { get => _ballExpansionState; set => _ballExpansionState = value; }

    public void SetState(IState state)
    {
        _currentState = state;
        _currentState.EnterState();
    }
    public void StateMachineTransitionState(IState to)
    {
        _currentState?.ExitState();
        _currentState = to;
        _currentState?.EnterState();
    }
    public void StateMachineUpdateTick()
    {
        _currentState?.UpdateState();
    }
}

public class BallStandardState : IState
{
    BallController _ballController;
    public BallStandardState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _ballController.Collider2D.sharedMaterial = _ballController.BallStandardBounce;
        _ballController.ChangeBallSize(1, 1);
    }

    public void ExitState()
    {
    }

    public void UpdateState()
    {
    }
}
public class BallConstantlyBounceState : IState
{
    BallController _ballController;
    float _currentTime;
    public BallConstantlyBounceState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _ballController.Collider2D.sharedMaterial = _ballController.BallConstantlyBounce;
        _currentTime = 0;
    }

    public void ExitState()
    {
        _currentTime = 0;
    }

    public void UpdateState()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > 5)
        {
            _ballController.BallControllerStateMachine.StateMachineTransitionState(_ballController.BallControllerStateMachine.BallStandardState);
        }
    }
}
public class BallLittleBounceState : IState
{
    float _currentTime;
    BallController _ballController;
    public BallLittleBounceState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _currentTime = 0;
        _ballController.Collider2D.sharedMaterial = _ballController.BallLittleBounce;
    }

    public void ExitState()
    {
        _currentTime = 0;
    }

    public void UpdateState()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > 5)
        {
            _ballController.BallControllerStateMachine.StateMachineTransitionState(_ballController.BallControllerStateMachine.BallStandardState);
        }
    }
}
public class BallExpansionState : IState
{
    float _currentTime;
    BallController _ballController;
    public BallExpansionState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _currentTime = 0;
        _ballController.ChangeBallSize(2, 2);
    }

    public void ExitState()
    {
        _currentTime = 0;
    }

    public void UpdateState()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > 5)
        {
            _ballController.BallControllerStateMachine.StateMachineTransitionState(_ballController.BallControllerStateMachine.BallStandardState);
        }
    }
}
public class BallShrinkageState : IState
{
    float _currentTime;
    BallController _ballController;
    public BallShrinkageState(BallController ballController)
    {
        _ballController = ballController;
    }
    public void EnterState()
    {
        _currentTime = 0;
        _ballController.ChangeBallSize(0.5f, 0.5f);
    }

    public void ExitState()
    {
        _currentTime = 0;
    }

    public void UpdateState()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > 5)
        {
            _ballController.BallControllerStateMachine.StateMachineTransitionState(_ballController.BallControllerStateMachine.BallStandardState);
        }
    }
}
public class BallTooFastState
{

}
