using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IOnGround
{
    bool IsOnGround { get; set; }
    void CheckForOnGround();
}


public class OnGround : IOnGround
{
    RaycastHit2D raycastHit2D;

    private bool _isOnGround;
    public bool IsOnGround { get; set; }

    Transform _groundCheckerTransform;
    float _distance;
    LayerMask _groundLayer;
    public OnGround(Transform groundCheckerTransform, float distance, LayerMask layerMask)
    {
        _groundCheckerTransform = groundCheckerTransform;
        _distance = distance;
        _groundLayer = layerMask;
    }
    public void CheckForOnGround()
    {
        raycastHit2D = Physics2D.Raycast(_groundCheckerTransform.position, _groundCheckerTransform.forward, _distance, _groundLayer);
        Debug.DrawRay(_groundCheckerTransform.position, _groundCheckerTransform.forward * _distance, Color.blue);
        if (raycastHit2D.collider != null)
        {

            IsOnGround = true;
        }
        else
        {
            IsOnGround = false;
        }
    }
}
