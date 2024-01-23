using System.Collections;
using UnityEngine;


public enum GenericBallPhysicsPowerTypeEnum
{
    BallLittleBouncingPower, BallConstantlyPower
}
public class GenericBallPhysicsPower : MonoBehaviour
{
    [SerializeField] GenericBallPhysicsPowerTypeEnum _genericBallPhysicsPowerTypeEnum;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController component))
        {
            switch (_genericBallPhysicsPowerTypeEnum)
            {
                case GenericBallPhysicsPowerTypeEnum.BallLittleBouncingPower:
                    component.BallControllerStateMachine.StateMachineTransitionState(component.BallControllerStateMachine.BallLittleBounceState);
                    break;
                case GenericBallPhysicsPowerTypeEnum.BallConstantlyPower:
                    component.BallControllerStateMachine.StateMachineTransitionState(component.BallControllerStateMachine.BallConstantlyBounceState);
                    break;
            }
        }
    }
}
