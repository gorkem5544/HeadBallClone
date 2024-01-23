using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "HeadBallClone/PlayerScriptableObject", order = 0)]
public class PlayerScriptableObject : ScriptableObject, IPlayerShootScriptableObject, IPlayerJumpScriptableObject, IPlayerMoveScriptableObject
{
    [SerializeField] float _zRotateValue;
    public float ZRotateValue => _zRotateValue;

    [SerializeField] float _shootSpeed;
    public float ShootSpeed => _shootSpeed;


    [SerializeField] float _jumpForce;
    public float PlayerJumpForce => _jumpForce;

    [SerializeField] float _moveSpeed;
    public float MoveSpeed => _moveSpeed;
}
public interface IPlayerMoveScriptableObject
{
    float MoveSpeed { get; }
}