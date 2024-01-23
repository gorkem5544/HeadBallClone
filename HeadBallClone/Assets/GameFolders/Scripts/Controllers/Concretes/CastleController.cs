using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CastleController : MonoBehaviour
{
    [SerializeField] CastleTypeEnum _castleTypeEnum;
    CastleStateMachine _castleStateMachine;

    public CastleStateMachine CastleStateMachine { get => _castleStateMachine; set => _castleStateMachine = value; }

    private void Awake()
    {
        _castleStateMachine = new CastleStateMachine(this);
    }
    private void Start()
    {
        _castleStateMachine.SetState(_castleStateMachine.CastleDefaultState);
    }
    public void SmallSizePower()
    {
        _castleStateMachine.StateMachineTransitionState(_castleStateMachine.CastleSmallSizeState);
    }
    public void HeightSizePower()
    {
        _castleStateMachine.StateMachineTransitionState(_castleStateMachine.CastleHeighSizeState);
    }
    public void FreezePower()
    {
        _castleStateMachine.StateMachineTransitionState(_castleStateMachine.CastleFreezeState);
    }
    private void Update()
    {
        _castleStateMachine.StateMachineUpdateTick();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BallController ballController))
        {
            GameManager.Instance.CharacterGoalEvent?.Invoke(_castleTypeEnum);
        }
    }
}
