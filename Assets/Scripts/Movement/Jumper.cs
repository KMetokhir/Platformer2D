using System;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float jumpingPower = 700f;

    private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody;

    private bool _canJump = false;
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
        if (IsJumpEnd())
        {
            JumpEnd?.Invoke();
            _isJumping = false;
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

    private void ThrowNotInitedException()
    {
        if (_isInited == false)
        {
            throw new Exception("Jumper dosen't inited");
        }
    }
}