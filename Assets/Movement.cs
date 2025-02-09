using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public float topSpeed = 5f;
    public float jumpForce = 5f;
    public float moveAcc = 0.2f;
    public float movePower = 1.1f;
    public float friction = 3f;
    public float jumpTime = 0.5f;
    public float wallSlide = 0.2f;
    public float wallPushForce = 5f;
    private float airTime = 0f;
    private float accel;
    private float decel;
    private bool _onFloor;
    private bool _onWall;
    private bool _jump;
    private bool _isJumping;
    private float _timeSinceJump;
    private float _moveDir;
    private float _wallCollideVelocity;
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
        _timeSinceJump += Time.deltaTime;

        if (_onFloor && _onWall)
        {
            _onWall = false;
        }

        if (Input.GetButtonDown("Jump") && !_jump && Math.Abs(_rb.linearVelocityY) < 0.01)
        {
            _jump = true;
            _isJumping = true;
            _timeSinceJump = 0;
        }
        else if (Input.GetButtonDown("Jump") && _onWall && !_onFloor) 
        {
            _rb.gravityScale = 2;
            _onWall = false;
            _jump = true;
            _isJumping = true;
            _timeSinceJump = 0;
            _rb.linearVelocity = (-Mathf.Sign(_wallCollideVelocity) * wallPushForce) * Vector2.right;
        }

        if ((!Input.GetButton("Jump") || _timeSinceJump > jumpTime) && _isJumping) {
            _isJumping = false;
        } else if (Input.GetButton("Jump") && _timeSinceJump <= jumpTime && _isJumping) {
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        if (Math.Abs(_moveDir) > 0.01)
        {
            float maxSpeed = topSpeed * _moveDir;
            float potSpeed = maxSpeed - _rb.linearVelocityX;
            float acceleration = (Mathf.Abs(maxSpeed) > 0.01f) ? accel : decel;
            float force = Mathf.Pow(Mathf.Abs(potSpeed) * acceleration, movePower) * Mathf.Sign(potSpeed);
            _rb.AddForce(force * Vector2.right);
        }
        else
        {
            float frictionForce = friction * Mathf.Sign(-_rb.linearVelocityX);
            _rb.AddForce(frictionForce * Vector2.right);
        }

        if (_onWall && !_onFloor)
        {
            _rb.linearVelocity = wallSlide * Vector2.up;
            _rb.gravityScale = 0f;
        }
        else {
            _rb.gravityScale = 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor")) { _jump = false; _onFloor = true; }
        else if (collision.gameObject.CompareTag("Wall")) 
        {
            if (!_onWall) {
                _wallCollideVelocity = collision.relativeVelocity.x;
            }
            _onWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) { _onWall = false; } 
        else if (collision.gameObject.CompareTag("Floor")) { _onFloor = false; }
    }
}
