using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public float airJumpThreshold = 0.1f;
    public int moveParticles = 10;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode up = KeyCode.W;
    public AudioClip jumpSound;
    public AudioClip landSound;
    private int wallHash = 0;
    private int floorHash = 0;
    private float gravityVal;
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
    private ParticleSystem _ps;
    private AudioSource _as;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ps = GetComponent<ParticleSystem>();
        _as = GetComponent<AudioSource>();
        gravityVal = _rb.gravityScale;
        accel = moveAcc;
        decel = -moveAcc;
    }

    void Update()
    {
        _moveDir = (Input.GetKey(right) ? 1 : 0) - (Input.GetKey(left) ? 1 : 0);
        _timeSinceJump += Time.deltaTime;

        if (_onFloor && _onWall)
        {
            _onWall = false;
        }

        if (!_onFloor && !_onWall)
        {
            airTime += Time.deltaTime;
        }
        else
        {
            airTime = 0f;
        }

        if (Input.GetKeyDown(up) && !_onWall && !_jump && (_onFloor || (!_onFloor && airTime <= airJumpThreshold))) 
        {
            _as.clip = jumpSound;
            _as.Play();
            _jump = true;
            _isJumping = true;
            _timeSinceJump = 0;
        }
        else if (Input.GetKeyDown(up) && _onWall && !_onFloor) 
        {
            _as.clip = jumpSound;
            _as.Play();
            _rb.gravityScale = gravityVal;
            _onWall = false;
            _jump = true;
            _isJumping = true;
            _timeSinceJump = 0;
            _rb.linearVelocity = Vector2.zero;
            _rb.AddForce((_wallCollideVelocity * wallPushForce) * Vector2.right);
        }

        if ((!Input.GetKey(up) || _timeSinceJump > jumpTime) && _isJumping) 
        {
            _isJumping = false;
        } 
        else if (Input.GetKey(up) && _timeSinceJump <= jumpTime && _isJumping) 
        {
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
        else if (!_onWall)
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
            _rb.gravityScale = gravityVal;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 face = collision.GetContact(0).normal;

        if (!collision.collider.CompareTag("Hazard") && !collision.collider.CompareTag("DestructingHazard") && !collision.collider.CompareTag("NonCollideable"))
        {
            if (face == transform.up)
            {
                // Floor
                _jump = false;
                _onFloor = true;
                floorHash = collision.gameObject.GetHashCode();
            }
            else if ((face == transform.right || face == -transform.right) && !collision.collider.CompareTag("NonHoldable"))
            {
                // Wall
                _onWall = true;
                if (face == transform.right)
                {
                    _wallCollideVelocity = 1;
                }
                else if (face == -transform.right)
                {
                    _wallCollideVelocity = -1;
                }
                wallHash = collision.gameObject.GetHashCode();
            }
            else if (face == -transform.up)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 0);
                _isJumping = false;
            }
            _as.clip = landSound;
            _as.Play();

        } 
        else
        {
            _onFloor = false;
            _onWall = false;
            _isJumping = false;
            airTime += airJumpThreshold + 0.1f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        int objHash = collision.gameObject.GetHashCode();

        if (objHash == floorHash)
        {
            // Floor
            _onFloor = false;
        }
        
        if (objHash == wallHash)
        {
            // Wall
            _onWall = false;
        }


    }
}
