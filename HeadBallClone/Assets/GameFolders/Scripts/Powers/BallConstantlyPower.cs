using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallConstantlyPower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            PowerManager.Instance.BallConstantlyBouncingPowerEvent();

        }
    }
}
