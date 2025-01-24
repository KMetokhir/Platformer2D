using TMPro;
using UnityEngine;

public class TextViewHealth : AbstractHealthView
{
    [SerializeField] private TMP_Text _healthPlate;

    protected override void OnValueChanged(uint value)
    {
        _healthPlate.text = value.ToString() + " / " + _health.MaxValue;
    }
}