using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundChecker))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float _gravityScale = 3;

    private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody;
   
    private bool _isMoving = false;

    public event Action<float> FacingChanged;
    public event Action StartsMoving;
    public event Action StopedMoving;

    public float HorizontalDirection { get; private set; }

    private void Awake()
    {
        _groundChecker = GetComponent<GroundChecker>();
        _rigidbody= GetComponent<Rigidbody2D>();

        _rigidbody.gravityScale = _gravityScale;
    }

    private void Update()
    {
        SetIsMoving();
    }

    private void FixedUpdate()
    {
        Move();
    }    

    public void SetHorizontalDirection(float horizontalMove)
    {
        if (horizontalMove != HorizontalDirection && horizontalMove != 0)
        {
            FacingChanged?.Invoke(horizontalMove);
        }

        if (HorizontalDirection != 0 && horizontalMove == 0)
        {
            StopedMoving?.Invoke();
        }

        HorizontalDirection = horizontalMove;
    }

    private void SetIsMoving()
    {
        if (_rigidbody.velocity.x != 0 && _groundChecker.IsGrounded && HorizontalDirection != 0)
        {
            if (_isMoving == false)
            {
                StartsMoving?.Invoke();
            }

            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    private void Move()
    {
        if (_groundChecker.IsGrounded)
        {
            _rigidbody.velocity = new Vector2(HorizontalDirection * speed, _rigidbody.velocity.y);
        }
    }
}