using System.Collections;
using System.Collections.Generic;
using Assembly_CSharp.Assets.GameFolders.Scripts.Inputs;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerVerticalMoveWithRigidBody2D, IPlayerJumpWithRigidBody2D, IEntityController
{

    [SerializeField] Transform _groundCheckerTransform;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _distance;


    IPlayerInput _playerInput;

    Rigidbody2D _rigidbody2D;
    public IPlayerInput PlayerInput => _playerInput;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;


    IPlayerVerticalMove _playerVerticalMove;
    IPlayerJump _playerJump;
    public IOnGround OnGround => _onGround;

    IOnGround _onGround;
    private bool _canJump;
    PlayerShoot _playerShoot;
    [SerializeField] Transform _leg;
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _onGround = new OnGround(_groundCheckerTransform, _distance, _groundLayer);
        _playerVerticalMove = new PlayerVerticalMoveWithRigidBody2D(this);
        _playerJump = new PlayerJumpWithRigidBody2D(this);
        _playerShoot = new PlayerShoot(_leg);
    }

    private void Update()
    {
        _onGround.CheckForOnGround();
        if (_playerInput.ShootKeyDown)
        {
            _playerShoot.ShootLeg();
        }
        else
        {
            _playerShoot.ResetLeg();
        }
    }
    private void FixedUpdate()
    {
        _playerJump.JumpFixedUpdate();

        _playerVerticalMove.VerticalMoveFixedUpdate();
    }
}








public interface IPlayerJump : IAbstractEntityJump
{

}
public interface IPlayerJumpWithRigidBody2D : IAbstractEntityJumpWithRigidBody2D
{
    IOnGround OnGround { get; }
    IPlayerInput PlayerInput { get; }
}
public class PlayerJumpWithRigidBody2D : AbstractEntityJumpWithRigidBody2D, IPlayerJump
{
    IPlayerJumpWithRigidBody2D _playerJumpWithRigidBody2D;
    public PlayerJumpWithRigidBody2D(IPlayerJumpWithRigidBody2D playerJumpWithRigidBody2D) : base(playerJumpWithRigidBody2D)
    {
        _playerJumpWithRigidBody2D = playerJumpWithRigidBody2D;
    }
    public override void JumpFixedUpdate()
    {
        if (_playerJumpWithRigidBody2D.OnGround.IsOnGround && _playerJumpWithRigidBody2D.PlayerInput.JumpKeyDown)
        {
            _playerJumpWithRigidBody2D.Rigidbody2D.velocity = Vector2.zero;
            _playerJumpWithRigidBody2D.Rigidbody2D.AddForce(Vector2.up * 200f, ForceMode2D.Force);
        }
    }
}




public interface IAbstractEntityJump
{
    void JumpFixedUpdate();
}
public interface IAbstractEntityJumpWithRigidBody2D
{
    Rigidbody2D Rigidbody2D { get; }
}
public abstract class AbstractEntityJumpWithRigidBody2D : IAbstractEntityJump
{
    IAbstractEntityJumpWithRigidBody2D _abstractEntityJumpWithRigidBody2D;
    public AbstractEntityJumpWithRigidBody2D(IAbstractEntityJumpWithRigidBody2D abstractEntityJumpWithRigidBody2D)
    {
        _abstractEntityJumpWithRigidBody2D = abstractEntityJumpWithRigidBody2D;
    }
    public abstract void JumpFixedUpdate();
}



#region HorizontalMove

public interface IPlayerVerticalMove : IAbstractEntityVerticalMove
{
}
public interface IPlayerVerticalMoveWithRigidBody2D : IAbstractEntityVerticalMoveWithRigidBody2D
{
    IPlayerInput PlayerInput { get; }
}
public class PlayerVerticalMoveWithRigidBody2D : AbstractEntityVerticalMoveWithRigidBody2D, IPlayerVerticalMove
{
    IPlayerVerticalMoveWithRigidBody2D _playerVerticalMoveWithRigidBody2D;

    public PlayerVerticalMoveWithRigidBody2D(IPlayerVerticalMoveWithRigidBody2D playerVerticalMoveWithRigidBody2D) : base(playerVerticalMoveWithRigidBody2D)
    {
        _playerVerticalMoveWithRigidBody2D = playerVerticalMoveWithRigidBody2D;
    }


    public override void VerticalMoveFixedUpdate()
    {
        _playerVerticalMoveWithRigidBody2D.Rigidbody2D.velocity = new Vector2(1 * _playerVerticalMoveWithRigidBody2D.PlayerInput.HorizontalInput * 200 * Time.deltaTime, _playerVerticalMoveWithRigidBody2D.Rigidbody2D.velocity.y);
    }
}







public interface IAbstractEntityVerticalMove
{
    void VerticalMoveFixedUpdate();
}
public interface IAbstractEntityVerticalMoveWithRigidBody2D
{
    Rigidbody2D Rigidbody2D { get; }
}
public abstract class AbstractEntityVerticalMoveWithRigidBody2D : IAbstractEntityVerticalMove
{
    IAbstractEntityVerticalMoveWithRigidBody2D _abstractEntityVerticalMoveWithRigidBody2D;
    public AbstractEntityVerticalMoveWithRigidBody2D(IAbstractEntityVerticalMoveWithRigidBody2D abstractEntityVerticalMoveWithRigidBody2D)
    {
        _abstractEntityVerticalMoveWithRigidBody2D = abstractEntityVerticalMoveWithRigidBody2D;
    }

    public abstract void VerticalMoveFixedUpdate();

}
#endregion

