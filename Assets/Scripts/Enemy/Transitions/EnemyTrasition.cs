using UnityEngine;

public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] protected DamageableDetector _detector;
    [SerializeField] private EnemyState _targetState;

    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        Enable();
        NeedTransit = false;
    }

    public void Activate()
    {
        enabled = true;
    }

    public void Deactivate()
    {
        enabled = false;
    }

    protected abstract void Enable();
}