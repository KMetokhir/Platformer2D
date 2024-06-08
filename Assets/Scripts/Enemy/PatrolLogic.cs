using System;
using UnityEngine;

public class PatrolLogic : MonoBehaviour
{
    private const float RightHorizontalDirection = 1;
    private const float LeftHorizontalDirection = -1;

    private Vector2 _patrolAria;
    private float _currentHorizontaldirection = 0;
    private Transform _patroller;

    private bool _isInited = false;

    public event Action<float> HorizontalDirectionChanged;

    private void Update()
    {
        if (_isInited == false)
        {
            return;
        }

        if (TryGetHorizontalDirection(out float direction))
        {
            if (_currentHorizontaldirection != direction)
            {
                _currentHorizontaldirection = direction;
                HorizontalDirectionChanged?.Invoke(_currentHorizontaldirection);
            }
        }
    }

    public void StartPatrol()
    {
        if (_isInited == false || _currentHorizontaldirection != 0)
        {
            return;
        }

        _currentHorizontaldirection = RightHorizontalDirection;
        HorizontalDirectionChanged?.Invoke(_currentHorizontaldirection);
    }

    public void Init(Vector2 patrolAria, Transform patroller)
    {
        if (_isInited)
        {
            return;
        }

        _isInited = true;

        _patrolAria = patrolAria;
        _patroller = patroller;
    }

    private bool TryGetHorizontalDirection(out float horizontalDirection)
    {
        bool isSuccess = false;
        horizontalDirection = 0;

        if (_patroller.position.x >= _patrolAria.y)
        {
            horizontalDirection = LeftHorizontalDirection;
            isSuccess = true;
        }
        else if (_patroller.position.x <= _patrolAria.x)
        {
            horizontalDirection = RightHorizontalDirection;
            isSuccess = true;
        }

        return isSuccess;
    }
}