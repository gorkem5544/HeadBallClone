using UnityEngine;

public class EnemyLegJumpWithRigidbody2D
{
    EnemyLegController _enemyLegController;
    public EnemyLegJumpWithRigidbody2D(EnemyLegController enemyLegController)
    {
        _enemyLegController = enemyLegController;
    }
    public void JumpTick()
    {
        _enemyLegController.Rigidbody2D.AddForce(Vector2.up * 300f, ForceMode2D.Force);
    }
}
