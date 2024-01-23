using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{

    public System.Action<IEntityController> CastleSmallSizePowerEvent;
    public System.Action<IEntityController> CastleHeightSizePowerEvent;
    public System.Action<IEntityController> CastleFreezePowerEvent;

    public static PowerManager Instance { get; private set; }

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
