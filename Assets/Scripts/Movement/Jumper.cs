using System;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float jumpingPower = 700f;

    private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody;

    private bool _isCanJump = false;
    private bool _isJumping = false;
    private bool _isInited = false;

    public event Action JumpStart; 
    public event Action JumpEnd;

    private void FixedUpdate()
    {       
        Jump();
    }

    private void LateUpdate()
    {
        if (_isJumping)
        {
            InvokeJumpEndEvent();
        }
    }

    public void Init(Rigidbody2D rigidbody, GroundChecker groundChecker)
    {
        if (_isInited)
        {
            return;
        }

        _isInited = true;
        _rigidbody = rigidbody;
        _groundChecker = groundChecker;      
    }

    public void SetJump()
    {
        ThrowNotInitedException();

        _isCanJump = true;
    }

    private void InvokeJumpEndEvent()
    {
        if(_groundChecker.IsGrounded && _rigidbody.velocity.y==0)
        {
            JumpEnd?.Invoke();
            _isJumping = false;
        }
    }
    
    private void Jump()
    {
        if (_groundChecker.IsGrounded && _isCanJump)
        {
            _isCanJump = false;  
            _isJumping = true;
            _rigidbody.AddForce(Vector2.up * jumpingPower);

            JumpStart?.Invoke();
        }
    }

    private void ThrowNotInitedException()
    {
        if (_isInited == false)
        {
            throw new Exception("Jumper dosen't inited");
        }
    }
}