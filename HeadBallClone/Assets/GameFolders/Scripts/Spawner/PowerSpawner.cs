using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    [SerializeField] Collider2D _powerSpawnCollider;

    [SerializeField] GameObject[] _powers;

    float _currentTime = 0;
    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > 5)
        {
            float x = Random.Range(_powerSpawnCollider.bounds.min.x, _powerSpawnCollider.bounds.max.x);
            float y = Random.Range(_powerSpawnCollider.bounds.min.y, _powerSpawnCollider.bounds.max.y);
            Instantiate(_powers[Random.Range(0, _powers.Length)], new Vector2(x, y), Quaternion.identity);
            _currentTime = 0;
        }
    }
}
