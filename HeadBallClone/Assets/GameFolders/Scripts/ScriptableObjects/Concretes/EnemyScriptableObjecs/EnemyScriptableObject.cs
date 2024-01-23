using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "HeadBallClone/EnemyScriptableObject", order = 0)]
public class EnemyScriptableObject : ScriptableObject, IEnemyShootScriptableObject
{
    [SerializeField] float _zRotateValue;
    public float ZRotateValue => _zRotateValue;

    [SerializeField] float _shootSpeed;
    public float ShootSpeed => _shootSpeed;
}
public interface IEnemyShootScriptableObject
{
    float ZRotateValue { get; }
    float ShootSpeed { get; }
}
