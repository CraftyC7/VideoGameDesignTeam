using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5;
    private Vector2 _velocity;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_velocity.x, _rb.linearVelocityY + _velocity.y);
    }

}
