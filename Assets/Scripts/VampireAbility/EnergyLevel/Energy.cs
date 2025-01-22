using System;
using System.Collections;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private uint _useTime;
    [SerializeField] private uint _rechargeTime;
   
    private float  _currentEnergyLevel;
    private float _maxEnergy = 100f;
    private float _minEnergyLevel = 0f;

    public event Action EnergyEmpty;
    public event Action EnergyRecharged;

    private Coroutine _coroutine;

    private void Start()
    {
        _currentEnergyLevel = _maxEnergy;
        EnergyRecharged?.Invoke();
    }

    public void Use()
    {
        if (_coroutine != null)
        {
            return;
        }
        else
        {
            _coroutine=  StartCoroutine(SmoothChange(_useTime, _minEnergyLevel));
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

            yield return null;
        }

        if (TryRecharge() == false)
        {
            EnergyRecharged?.Invoke();
            _coroutine = null;
        }
        else
        {
            EnergyEmpty?.Invoke();
        }
    }

    private bool TryRecharge()
    {
        bool isSucces;

        if (_currentEnergyLevel != _minEnergyLevel)
        {
            isSucces = false;
            return isSucces;
        }
        else
        {
            _coroutine = StartCoroutine(SmoothChange( _rechargeTime, _maxEnergy));
            isSucces = true;
            return isSucces;
        }
    }
}