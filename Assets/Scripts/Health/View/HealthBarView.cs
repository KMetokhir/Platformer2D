using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : AbstractHealthView
{
    [SerializeField] protected Slider _healthBar;

    protected override void OnValueChanged(uint value)
    {
        _healthBar.maxValue = _health.MaxValue;
        _healthBar.value = value;
    }
}