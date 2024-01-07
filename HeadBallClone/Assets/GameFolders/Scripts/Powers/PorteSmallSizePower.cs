using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PowerType
{
    Yes, False
}
public class PorteSmallSizePower : MonoBehaviour
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
                PowerManager.Instance.CastleSmallSizePowerEvent(ballController.Rival);
            }
            else
            {
                PowerManager.Instance.CastleSmallSizePowerEvent(ballController.LastShootCharacter);
            }
        }
    }
}
