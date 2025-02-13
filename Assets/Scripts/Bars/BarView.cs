using UnityEngine;
using UnityEngine.UI;

public abstract class BarView : IChangeableView
{
    [SerializeField] protected Slider _bar;

    protected override void OnValueChanged(float value)
    {
        _bar.maxValue = _changeable.MaxValue;
        _bar.value = value;
    }
}