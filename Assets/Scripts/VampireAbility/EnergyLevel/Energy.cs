using System;
using System.Collections;
using UnityEngine;

public class Energy : MonoBehaviour, IChangeable
{
    [SerializeField] private uint _useTime;
    [SerializeField] private uint _rechargeTime;

    private float _currentEnergyLevel;
    private uint _maxEnergy = 100;
    private float _minEnergyLevel = 0f;

    public event Action EnergyEmpty;
    public event Action EnergyRecharged;
    public event Action<float> ValueChanged;

    public uint MaxValue => _maxEnergy;

    private Coroutine _coroutine;

    private void Start()
    {
        _currentEnergyLevel = _maxEnergy;
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

        if (TryStartRecharge() == false)
        {
            EnergyRecharged?.Invoke();
            _coroutine = null;
        }
        else
        {
            EnergyEmpty?.Invoke();
        }
    }

    private bool TryStartRecharge()
    {
        bool isSucces;

        if (_currentEnergyLevel == _maxEnergy)
        {
            isSucces = false;

            return isSucces;
        }
        else if (_currentEnergyLevel == _minEnergyLevel)
        {
            _coroutine = StartCoroutine(SmoothChange(_rechargeTime, _maxEnergy));
            isSucces = true;

            return isSucces;
        }
        else
        {
            isSucces = false;

            return isSucces;
        }
    }
}