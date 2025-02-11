using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;
    [SerializeField] private EnemyStateMachine _stateMachine;

    [SerializeField] private Mover _mover;

    protected float CurrentHorizontalDirection => _mover.HorizontalDirection;

    private void Awake()
    {
        enabled = false;
    }

    public virtual void Enter()
    {
        if (enabled == false)
        {
            enabled = true;
        }
    }

    public virtual void Exit()
    {
        enabled = false;
    }

    protected void TransitToTarGetState()
    {
        _stateMachine.Transit(this, _targetState);
    }

    protected void Move(float direction)
    {
        _mover.SetHorizontalDirection(direction);
    }
}