using System.Collections;
using System.Collections.Generic;
using Assembly_CSharp.Assets.GameFolders.Scripts.Inputs;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerVerticalMoveWithRigidBody2D, IPlayerJumpWithRigidBody2D, IEntityController
{
    [SerializeField] private PlayerScriptableObject _playerScriptableObject;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Transform _leg;



    private Rigidbody2D _rigidbody2D;
    private IEntityShoot _playerShoot;
    private IPlayerInput _playerInput;
    private IPlayerVerticalMove _playerVerticalMove;
    private IPlayerJump _playerJump;



    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public IOnGround GroundDetector => _groundDetector;
    public IPlayerInput PlayerInput => _playerInput;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
        _playerVerticalMove = new PlayerVerticalMoveWithRigidBody2D(this, _playerScriptableObject);
        _playerJump = new PlayerJumpWithRigidBody2D(this, _playerScriptableObject);
        _playerShoot = new PlayerShoot(_leg, _playerScriptableObject, this);

    }

    private void Update()
    {
        _playerShoot.UpdateTick();
        _playerJump.JumpUpdateTick();
    }
    private void FixedUpdate()
    {
        _playerJump.JumpFixedUpdate();
        _playerVerticalMove.MoveFixedTick();
    }
}

public interface IPlayerJump
{
    void JumpFixedUpdate();
    void JumpUpdateTick();
}
public interface IPlayerJumpWithRigidBody2D : IRigidBodyService, IPlayerInputService
{
    IOnGround GroundDetector { get; }

}
public class PlayerJumpWithRigidBody2D : IPlayerJump
{
    IPlayerJumpWithRigidBody2D _playerJumpWithRigidBody2D;
    IPlayerJumpScriptableObject _playerJumpScriptableObject;
    private bool _canJump;
    public PlayerJumpWithRigidBody2D(IPlayerJumpWithRigidBody2D playerJumpWithRigidBody2D, IPlayerJumpScriptableObject playerJumpScriptableObject)
    {
        _playerJumpWithRigidBody2D = playerJumpWithRigidBody2D;
        _playerJumpScriptableObject = playerJumpScriptableObject;
    }
    public void JumpFixedUpdate()
    {
        if (_canJump)
        {
            _playerJumpWithRigidBody2D.Rigidbody2D.velocity = Vector2.zero;
            _playerJumpWithRigidBody2D.Rigidbody2D.AddForce(Vector2.up * _playerJumpScriptableObject.PlayerJumpForce, ForceMode2D.Force);
            _canJump = false;
        }
    }

    public void JumpUpdateTick()
    {
        if (_playerJumpWithRigidBody2D.GroundDetector.IsOnGround && _playerJumpWithRigidBody2D.PlayerInput.JumpKeyDown)
        {
            _canJump = true;
        }
    }
}


public interface IPlayerVerticalMove
{
    void MoveFixedTick();
}
public interface IPlayerVerticalMoveWithRigidBody2D : IRigidBodyService, IPlayerInputService
{
}
public class PlayerVerticalMoveWithRigidBody2D : IPlayerVerticalMove
{
    IPlayerVerticalMoveWithRigidBody2D _playerVerticalMoveWithRigidBody2D;
    IPlayerMoveScriptableObject _playerMoveScriptableObject;
    public PlayerVerticalMoveWithRigidBody2D(IPlayerVerticalMoveWithRigidBody2D playerVerticalMoveWithRigidBody2D, IPlayerMoveScriptableObject playerMoveScriptableObject)
    {
        _playerVerticalMoveWithRigidBody2D = playerVerticalMoveWithRigidBody2D;
        _playerMoveScriptableObject = playerMoveScriptableObject;
    }
    public void MoveFixedTick()
    {
        _playerVerticalMoveWithRigidBody2D.Rigidbody2D.velocity = new Vector2(_playerVerticalMoveWithRigidBody2D.PlayerInput.HorizontalInput * _playerMoveScriptableObject.MoveSpeed * Time.deltaTime, _playerVerticalMoveWithRigidBody2D.Rigidbody2D.velocity.y);
    }
}


public interface IRigidBodyService
{
    Rigidbody2D Rigidbody2D { get; }
}
public interface IPlayerInputService
{
    IPlayerInput PlayerInput { get; }
}