using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public interface IEntityShoot
{
    void UpdateTick();
}

public class PlayerShoot : IEntityShoot
{
    IPlayerShootScriptableObject _playerShootScriptableObject;
    IPlayerInputService _playerInputService;
    Transform _footTransform;
    public PlayerShoot(Transform footTransform, IPlayerShootScriptableObject playerShootScriptableObject, IPlayerInputService playerInputService)
    {
        _footTransform = footTransform;
        _playerShootScriptableObject = playerShootScriptableObject;
        _playerInputService = playerInputService;
    }

    public void UpdateTick()
    {
        if (_playerInputService.PlayerInput.ShootKeyDown)
        {
            LegAction(_playerShootScriptableObject.ZRotateValue, _playerShootScriptableObject.ShootSpeed);
        }
        else
        {
            LegAction(0, _playerShootScriptableObject.ShootSpeed);
        }
    }
    private void LegAction(float zRotateValue, float rotateSpeed = 25)
    {
        Quaternion shootRotation = Quaternion.Euler(0, 0, zRotateValue);
        _footTransform.transform.rotation = Quaternion.Lerp(_footTransform.transform.rotation, shootRotation, rotateSpeed * Time.deltaTime);
    }
}
