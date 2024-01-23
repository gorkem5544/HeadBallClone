using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallController : MonoBehaviour, IEntityController
{
    Rigidbody2D _rigidbody2D;
    Collider2D _collider2D;
    IEntityController _lastShootCharacter;
    IEntityController _rival;
    [SerializeField] PhysicsMaterial2D _ballLittleBounce;
    [SerializeField] PhysicsMaterial2D _ballStandardBounce;
    [SerializeField] PhysicsMaterial2D _ballConstantlyBounce;


    IEntityController _enemyController;
    IEntityController _playerController;
    public BallPhysicsStateMachine BallControllerStateMachine => _ballPhysicsStateMachine;
    public IEntityController LastShootCharacter { get => _lastShootCharacter; set => _lastShootCharacter = value; }
    public IEntityController Rival { get => _rival; set => _rival = value; }
    public PhysicsMaterial2D BallLittleBounce { get => _ballLittleBounce; set => _ballLittleBounce = value; }
    public PhysicsMaterial2D BallConstantlyBounce { get => _ballConstantlyBounce; set => _ballConstantlyBounce = value; }
    public PhysicsMaterial2D BallStandardBounce { get => _ballStandardBounce; set => _ballStandardBounce = value; }
    public Rigidbody2D Rigidbody2D { get => _rigidbody2D; set => _rigidbody2D = value; }
    public Collider2D Collider2D { get => _collider2D; set => _collider2D = value; }
    public BallSizeStateMachine BallSizeStateMachine { get => _ballSizeStateMachine; set => _ballSizeStateMachine = value; }


    BallPhysicsStateMachine _ballPhysicsStateMachine;
    BallSizeStateMachine _ballSizeStateMachine;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _enemyController = GameObject.FindGameObjectWithTag("Enemy").transform.GetComponent<EnemyController>();
        _playerController = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>();
        _ballPhysicsStateMachine = new BallPhysicsStateMachine(this);

        _ballSizeStateMachine = new BallSizeStateMachine(this);
    }
    private void Start()
    {
        _ballPhysicsStateMachine.SetState(_ballPhysicsStateMachine.BallStandardState);
        _ballSizeStateMachine.SetState(_ballSizeStateMachine.BallStandardSizeState);
    }



    private void Update()
    {
        _ballPhysicsStateMachine.StateMachineUpdateTick();
        _ballSizeStateMachine.StateMachineUpdateTick();
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

    public void UpdateSize(float sizeX, float sizeY)
    {
        transform.localScale = new Vector2(sizeX, sizeY);
    }
    public void UpdateSpeed(float speed)
    {

    }

}
