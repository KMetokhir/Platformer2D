﻿using UnityEngine;

public class PatrolBehaviour : AbstractBehaviour
{
    private const float RightHorizontalDirection = 1;
    private const float LeftHorizontalDirection = -1;

    private Vector2 _patrolAria;
    private DamageableDetector _detector;

    private bool _isInited = false;

    private void Update()
    {
        if (_isInited == false || base.IsActive == false)
        {
            return;
        }

        if (TryChangeDirection(_patrolAria.x, _patrolAria.y))
        {
            SetEyeDirection();
        }
    }

    public override void Enter()
    {
        if (_isInited == false || base.IsActive)
        {
            return;
        }

        base.Enter();

        Move(RightHorizontalDirection);

        SetEyeDirection();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void Init(Vector2 patrolAria, Mover mover, DamageableDetector detector)
    {
        Init(mover);

        if (_isInited)
        {
            return;
        }

        _isInited = true;

        _patrolAria = patrolAria;
        _detector = detector;
    }

    private void SetEyeDirection()
    {
        Vector2 direction = new Vector2(CurrentHorizontalDirection, 0);
        _detector.SetEyeDirection(direction);
    }

    private bool TryChangeDirection(float leftBound, float rightBound)
    {
        if (TryGetHorizontalDirection(leftBound, rightBound, out float direction))
        {
            if (CurrentHorizontalDirection != direction)
            {
                Move(direction);

                return true;
            }
        }

        return false;
    }

    private bool TryGetHorizontalDirection(float leftBound, float rightBound, out float horizontalDirection)
    {
        bool isSuccess = false;
        horizontalDirection = 0;

        if (transform.position.x >= rightBound)
        {
            horizontalDirection = LeftHorizontalDirection;
            isSuccess = true;
        }
        else if (transform.position.x <= leftBound)
        {
            horizontalDirection = RightHorizontalDirection;
            isSuccess = true;
        }

        return isSuccess;
    }
}