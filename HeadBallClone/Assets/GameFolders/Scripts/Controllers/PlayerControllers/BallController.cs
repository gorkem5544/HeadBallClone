using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallController : MonoBehaviour, IEntityController
{

    Rigidbody2D _rigidbody2D;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out PlayerController player))
        {
            ContactPoint2D contact = other.contacts[0];
            Vector3 pos = contact.point;
            _rigidbody2D.AddForce(pos * 2);
        }
        if (other.collider.TryGetComponent(out FootController leg))
        {
            Vector2 yon = other.contacts[0].point - (Vector2)transform.position;
            yon = yon.normalized;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Abs(yon.x), Mathf.Abs(yon.y)) * 5, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        }
    }

}
