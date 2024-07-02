using UnityEngine;

public class Health : MonoBehaviour
{
    private const uint MaxValue = 100;

    [SerializeField] private uint _value;

    public void Decrease(uint damage)
    {
        if (_value >= damage)
        {
            _value -= damage;
        }
        else
        {
            _value = 0;
        }

        Debug.Log(_value);
    }

    public void Increase(uint treatment)
    {
        _value = (_value + treatment) > _value ? MaxValue : _value += treatment;
    }
}