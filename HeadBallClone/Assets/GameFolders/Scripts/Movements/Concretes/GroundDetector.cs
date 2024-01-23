using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IOnGround
{
    bool IsOnGround { get; }
}
public class GroundDetector : MonoBehaviour, IOnGround
{
    RaycastHit2D _groundHit;
    private bool _isOnGround;
    public bool IsOnGround => _isOnGround;
    [SerializeField] Transform[] _footTransforms;
    [SerializeField] float _distance;
    [SerializeField] LayerMask _groundLayer;

    private void Update()
    {
        foreach (Transform foot in _footTransforms)
        {
            CheckForOnGround(foot);
            if (_isOnGround)
            {
                break;
            }
        }
    }
    private void CheckForOnGround(Transform footTransform)
    {
        _groundHit = Physics2D.Raycast(footTransform.position, footTransform.forward, _distance, _groundLayer);
        Debug.DrawRay(footTransform.position, footTransform.forward * _distance, Color.blue);
        if (_groundHit.collider != null)
        {
            _isOnGround = true;
        }
        else
        {
            _isOnGround = false;
        }
    }
}
