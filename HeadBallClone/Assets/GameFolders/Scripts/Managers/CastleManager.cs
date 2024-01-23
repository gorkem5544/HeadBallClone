using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    [SerializeField] CastleController _enemyCastle;
    [SerializeField] CastleController _playerCastle;

    private void Start()
    {
        PowerManager.Instance.CastleSmallSizePowerEvent += CastleSmallSizePowerEvent;
        PowerManager.Instance.CastleHeightSizePowerEvent += CastleHeightSizePowerEvent;
        PowerManager.Instance.CastleFreezePowerEvent += CastleFreezePowerEvent;
    }
    private void OnDisable()
    {
        PowerManager.Instance.CastleSmallSizePowerEvent -= CastleSmallSizePowerEvent;
        PowerManager.Instance.CastleHeightSizePowerEvent -= CastleHeightSizePowerEvent;
        PowerManager.Instance.CastleFreezePowerEvent -= CastleFreezePowerEvent;
    }
    private void CastleSmallSizePowerEvent(IEntityController controller)
    {
        if (controller.transform.CompareTag("Player"))
        {
            _playerCastle.SmallSizePower();
        }
        else
        {
            _enemyCastle.SmallSizePower();
        }
    }
    private void CastleHeightSizePowerEvent(IEntityController controller)
    {
        if (controller.transform.CompareTag("Player"))
        {
            _playerCastle.HeightSizePower();
        }
        else
        {
            _enemyCastle.HeightSizePower();

        }
    }
    private void CastleFreezePowerEvent(IEntityController controller)
    {
        if (controller.transform.CompareTag("Player"))
        {
            _playerCastle.FreezePower();
        }
        else
        {
            _enemyCastle.FreezePower();

        }
    }
}
