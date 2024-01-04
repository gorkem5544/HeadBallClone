using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CastleTypeEnum
{
    PlayerCastle, EnemyCastle
}
public class PorteController : MonoBehaviour
{
    [SerializeField] CastleTypeEnum _castleTypeEnum;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            GameManager.Instance.PlayerGoalEvent?.Invoke(_castleTypeEnum);
        }
    }
}


public class PorteSmallSizeState
{
}
public class PorteHeighSizeState
{
}
public class PorteFreezeState
{
}
