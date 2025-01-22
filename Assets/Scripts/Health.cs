using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private uint _maxValue = 100;
    [SerializeField] private uint _value;

    public uint Decrease(uint damage)
    {
        if (_value >= damage)
        {
            _value -= damage;
            return damage;
        }
        else
        {
            uint currentValue = _value;
            _value = 0;

            return currentValue;
        }        
    }

    public void Increase(uint treatment)
    {
        _value = (_value + treatment) > _value ? _maxValue : _value += treatment;
    }
}