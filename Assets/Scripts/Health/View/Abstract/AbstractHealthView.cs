using UnityEngine;

public abstract class AbstractHealthView : MonoBehaviour
{
    [SerializeField] protected Health _health;

    private void OnEnable()
    {
        _health.ValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= OnValueChanged;
        Disable();
    }

    protected virtual void Disable()
    {
    }

    protected abstract void OnValueChanged(uint value);
}