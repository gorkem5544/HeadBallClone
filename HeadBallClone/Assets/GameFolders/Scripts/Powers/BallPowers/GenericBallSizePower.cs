using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GenericBallSizePowerTypeEnum
{
    BallHugeSize, BallSmallSize
}
public class GenericBallSizePower : MonoBehaviour
{
    [SerializeField] GenericBallSizePowerTypeEnum _genericBallSizePowerTypeEnum;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController component))
        {
            switch (_genericBallSizePowerTypeEnum)
            {
                case GenericBallSizePowerTypeEnum.BallHugeSize:
                    component.BallSizeStateMachine.StateMachineTransitionState(component.BallSizeStateMachine.BallHugeSizeState);
                    break;
                case GenericBallSizePowerTypeEnum.BallSmallSize:
                    component.BallSizeStateMachine.StateMachineTransitionState(component.BallSizeStateMachine.BallSmallSizeState);
                    break;
            }
        }
    }
}