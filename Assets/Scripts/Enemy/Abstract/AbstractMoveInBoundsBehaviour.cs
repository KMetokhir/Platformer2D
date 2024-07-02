using UnityEngine;

public class AbstractMoveInBoundsBehaviour : MonoBehaviour
{
    protected const float RightHorizontalDirection = 1;
    protected const float LeftHorizontalDirection = -1;

    private Transform _behaviourOwner;
    private Mover _mover;

    private bool _isInit = false;

    public bool IsActive { get; private set; }
    public float CurrentHorizontalDirection => _mover.HorizontalDirection;
    public Vector3 Position => _behaviourOwner.position;

    protected void Init(Transform behaviourOwner, Mover mover)
    {
        _behaviourOwner = behaviourOwner;
        _mover = mover;
        IsActive = false;
        _isInit = true;
    }

    public virtual void Enter()
    {
        if (_isInit == false || IsActive)
        {
            return;
        }

        IsActive = true;
    }

    public virtual void Exit()
    {
        IsActive = false;
    }

    protected void Move(float direction)
    {
        _mover.SetHorizontalDirection(direction);
    }

    protected bool TryChangeDirection(float leftBound, float rightBound)
    {
        if (_behaviourOwner == null)
        {
            Debug.LogError("Behaviour owner is null");
        }

        if (TryGetHorizontalDirection(leftBound, rightBound, out float direction))
        {
            if (_mover.HorizontalDirection != direction)
            {
                _mover.SetHorizontalDirection(direction);

                return true;
            }
        }

        return false;
    }

    private bool TryGetHorizontalDirection(float leftBound, float rightBound, out float horizontalDirection)
    {
        bool isSuccess = false;
        horizontalDirection = 0;

        if (_behaviourOwner.position.x >= rightBound)
        {
            horizontalDirection = LeftHorizontalDirection;
            isSuccess = true;
        }
        else if (_behaviourOwner.position.x <= leftBound)
        {
            horizontalDirection = RightHorizontalDirection;
            isSuccess = true;
        }

        return isSuccess;
    }
}