using UnityEngine;

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


