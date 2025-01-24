using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private uint _maxValue = 100;
    [SerializeField] private uint _value;

    public event Action<uint> ValueChanged;

    public uint MaxValue => _maxValue;

    private void Start()
    {
        ValueChanged?.Invoke(_value);
    }

    public uint Decrease(uint damage)
    {
        if (_value >= damage)
        {
            _value -= damage;
            ValueChanged?.Invoke(_value);

            return damage;
        }
        else
        {
            uint currentValue = _value;
            _value = 0;
            ValueChanged?.Invoke(_value);

            return currentValue;
        }
    }

    public void Increase(uint treatment)
    {
        _value = (_value + treatment) > _maxValue ? _maxValue : _value += treatment;

        ValueChanged?.Invoke(_value);
    }
}