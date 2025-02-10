using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyTransition[] _transitions;
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

            foreach (var transition in _transitions)
            {
                transition.Activate();
            }
        }
    }

    public virtual void Exit()
    {
        foreach (var transition in _transitions)
        {
            transition.Deactivate();
        }

        enabled = false;
    }

    public EnemyState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }

    protected void Move(float direction)
    {
        _mover.SetHorizontalDirection(direction);
    }
}