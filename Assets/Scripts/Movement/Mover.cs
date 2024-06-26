using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float _gravityScale = 3;    

    private GroundChecker _groundChecker;

    private float _horizontalMove;   

    private Rigidbody2D _rigidbody;
    private bool _isInited = false;
    private bool _isMoving = false;   

    public event Action<float> FacingChanged;
    public event Action StartsMoving;
    public event Action StopedMoving;

    private void Update()
    {
        if (_isInited == false)
        {
            return;
        }

        InvokeMovingEvents();       
    }

    private void FixedUpdate()
    {
        Move();      
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

        _rigidbody.gravityScale = _gravityScale;
    }

    public void SetHorizontalDirection(float horizontalMove)
    {
        ThrowNotInitedException();

        if (horizontalMove != _horizontalMove)
        {
            FacingChanged?.Invoke(horizontalMove);
        }

        _horizontalMove = horizontalMove;
    }

    private void InvokeMovingEvents()
    {
        if (_rigidbody.velocity.x != 0 && _rigidbody.velocity.y == 0)
        {
            if (_isMoving == false)
            {
                _isMoving = true;
                StartsMoving?.Invoke();
            }
        }
        else if (_rigidbody.velocity.y == 0)
        {
            if (_isMoving == true)
            {
                _isMoving = false;
                StopedMoving?.Invoke();
            }
        }
    }

    private void Move()
    {
        if (_groundChecker.IsGrounded)
        {
            _rigidbody.velocity = new Vector2(_horizontalMove * speed, _rigidbody.velocity.y);
        }
    }

    private void ThrowNotInitedException()
    {
        if (_isInited == false)
        {
            throw new Exception("Mover dosen't inited");
        }
    }   
}