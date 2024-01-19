using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GenericBallPowerTypeEnum
{
    BallShrinkagePower, BallConstantlyPower
}
public class GenericBallPower : MonoBehaviour
{
    [SerializeField] GenericBallPowerTypeEnum _genericBallPowerTypeEnum;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            PowerManager.Instance.GenericBallPowerEvent(_genericBallPowerTypeEnum);
        }
    }
}
