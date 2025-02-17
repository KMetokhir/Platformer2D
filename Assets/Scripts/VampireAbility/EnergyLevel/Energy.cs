using System;
using System.Collections;
using UnityEngine;

public class Energy : MonoBehaviour, IChangeable
{
    [SerializeField] private uint _useTime;
    [SerializeField] private uint _rechargeTime;

    private float _currentEnergyLevel;
    private uint _maxEnergyLevel = 100;
    private float _minEnergyLevel = 0f;

    private Coroutine _coroutine;

    public event Action EnergyEmpty;
    public event Action EnergyRecharged;
    public event Action<float> ValueChanged;

    public uint MaxValue => _maxEnergyLevel;

    private void Start()
    {
        _currentEnergyLevel = _maxEnergyLevel;
        EnergyRecharged?.Invoke();
        ValueChanged?.Invoke(_currentEnergyLevel);
    }

    public void Use()
    {
        if (_coroutine != null)
        {
            return;
        }
        else
        {
            _coroutine = StartCoroutine(SmoothChange(_useTime, _minEnergyLevel));
        }
    }

    private IEnumerator SmoothChange(uint processTime, float targetValue)
    {
        float counter = 0;

        while (_currentEnergyLevel != targetValue)
        {
            counter += Time.deltaTime;
            float speed = Mathf.Abs(targetValue - _currentEnergyLevel) / (processTime - counter) * Time.deltaTime;
            _currentEnergyLevel = Mathf.MoveTowards(_currentEnergyLevel, targetValue, speed);
            ValueChanged?.Invoke(_currentEnergyLevel);

            yield return null;
        }

        if (_currentEnergyLevel == _minEnergyLevel)
        {
            EnergyEmpty?.Invoke();
            _coroutine = StartCoroutine(SmoothChange(_rechargeTime, _maxEnergyLevel));
        }
        else if (_currentEnergyLevel == _maxEnergyLevel)
        {
            EnergyRecharged?.Invoke();
        }
        else
        {
            throw new Exception("SmoothChange coroutine error in process");
        }

        _coroutine = null;
    }
}