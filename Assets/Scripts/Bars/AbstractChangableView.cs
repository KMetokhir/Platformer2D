using UnityEngine;

public abstract class IChangeableView : MonoBehaviour
{
    [SerializeField] protected IChangeable _changeable;

    private void Awake()
    {
        _changeable = GetComponent<IChangeable>();
    }

    private void OnEnable()
    {
        _changeable.ValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _changeable.ValueChanged -= OnValueChanged;
        Disable();
    }

    protected virtual void Disable()
    {
    }

    protected abstract void OnValueChanged(float value);
}