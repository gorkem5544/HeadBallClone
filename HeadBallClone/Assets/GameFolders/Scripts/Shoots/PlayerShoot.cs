using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public interface IPlayerShoot
{
    IFootController FootController { get; }
}
public class PlayerShoot
{
    IFootController _footController;
    Transform _leg;
    public PlayerShoot(Transform leg)
    {
        _leg = leg;
    }

    public void ShootLeg()
    {
        Quaternion shootRotation = Quaternion.Euler(0, 0, 50);
        _leg.transform.rotation = Quaternion.Lerp(_leg.transform.rotation, shootRotation, 25f * Time.deltaTime);
    }
    public void ResetLeg()
    {
        Quaternion shootRotation = Quaternion.Euler(0, 0, 0);
        _leg.transform.rotation = Quaternion.Lerp(_leg.transform.rotation, shootRotation, 25f * Time.deltaTime);
    }
}
