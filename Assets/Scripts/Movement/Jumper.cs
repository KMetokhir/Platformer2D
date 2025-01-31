using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundChecker))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float jumpingPower = 700f;

    private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody;

    private bool _canJump = false;
    private bool _isJumping = false; 

    public event Action JumpStart;
    public event Action JumpEnd;

    private void Awake()
    {
        _groundChecker = GetComponent<GroundChecker>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void LateUpdate()
    {
        if (IsJumpEnd())
        {
            JumpEnd?.Invoke();
            _isJumping = false;
        }
    }    

    public void SetJump()
    {       
        _canJump = true;
    }

    private bool IsJumpEnd()
    {
        return _isJumping && _groundChecker.IsGrounded;
    }

    private void Jump()
    {
        if (_groundChecker.IsGrounded && _canJump)
        {
            _rigidbody.AddForce(Vector2.up * jumpingPower);
            _canJump = false;
            _isJumping = true;            

            JumpStart?.Invoke();
        }
    }    
}