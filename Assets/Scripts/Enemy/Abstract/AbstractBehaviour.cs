using UnityEngine;

public abstract class AbstractBehaviour : MonoBehaviour
{
    private Mover _mover;
    private bool _isInit = false;

    public bool IsActive { get; private set; }

    protected float CurrentHorizontalDirection => _mover.HorizontalDirection;

    protected void Init(Mover mover)
    {
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
}