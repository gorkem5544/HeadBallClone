using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot
{
    IEnemyShootScriptableObject _enemyShootScriptableObject;
    Transform _footTransform;
    public EnemyShoot(Transform footTransform, IEnemyShootScriptableObject enemyShootScriptableObject)
    {
        _footTransform = footTransform;
        _enemyShootScriptableObject = enemyShootScriptableObject;
    }
    // public override void ResetAction()
    // {
    //     LegAction(0, _enemyShootScriptableObject.ShootSpeed);
    // }

    // public override void ShotAction()
    // {
    //     LegAction(_enemyShootScriptableObject.ZRotateValue, _enemyShootScriptableObject.ShootSpeed);

    // }
}