using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteFreezePower : MonoBehaviour
{

    PowerType _powerType;
    private void Start()
    {
        _powerType = (PowerType)Random.Range(0, 2);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            if (_powerType == PowerType.Yes)
            {
                PowerManager.Instance.CastleFreezePowerEvent(ballController.Rival);
            }
            else
            {
                PowerManager.Instance.CastleFreezePowerEvent(ballController.LastShootCharacter);
            }
        }
    }
}

public abstract class BaseBower : MonoBehaviour
{
    protected PowerType _powerType;
    protected virtual void Start()
    {
        _powerType = (PowerType)Random.Range(0, 2);
    }
}