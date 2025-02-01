using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float topSpeed = 5f;
    public float jumpForce = 5f;
    public float moveAcc = 0.2f;
    public float movePower = 1.1f;
    private float accel;
    private float decel;
    private bool _jump;
    private float _moveDir;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        accel = moveAcc;
        decel = -moveAcc;
    }

    void Update()
    {
        _moveDir = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && !_jump) {
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, jumpForce);
            _jump = true;
        }
    }

    private void FixedUpdate()
    {
        float maxSpeed = topSpeed * _moveDir;
        float potSpeed = maxSpeed - _rb.linearVelocityX;
        float acceleration = (Mathf.Abs(maxSpeed) > 0.01f) ? accel : decel;
        float force = Mathf.Pow(Mathf.Abs(potSpeed) * acceleration, movePower) * Mathf.Sign(potSpeed);
        _rb.AddForce(force * Vector2.right);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _jump = false;
    }
}
