using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    #region Castle Power
    public System.Action<IEntityController> CastleSmallSizePowerEvent;
    public System.Action<IEntityController> CastleHeightSizePowerEvent;
    public System.Action<IEntityController> CastleFreezePowerEvent;
    #endregion

    #region Ball Power
    public System.Action BallLittleBouncingPowerEvent;
    public System.Action BallConstantlyBouncingPowerEvent;

    public System.Action BallExpansionPowerEvent;
    public System.Action BallShrinkagePowerEvent;
    #endregion


    public static PowerManager Instance { get; private set; }
    public System.Action<GenericBallPowerTypeEnum> GenericBallPowerEvent;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
