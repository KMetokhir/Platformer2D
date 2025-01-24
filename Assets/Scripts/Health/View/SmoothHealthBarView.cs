using System.Collections;
using UnityEngine;

public class SmoothHealthBarView : HealthBarView
{
    [SerializeField] private float _fillSpeed = 0.2f;

    private Coroutine _currentCoroutine;

    protected override void Disable()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
    }

    protected override void OnValueChanged(uint value)
    {
        _healthBar.maxValue = _health.MaxValue;

        if (_currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(SmoothFill(value));
        }
        else
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(SmoothFill(value));
        }
    }

    private IEnumerator SmoothFill(float targetValue)
    {
        while (_healthBar.value != targetValue)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, targetValue, _fillSpeed);

            yield return null;
        }
    }
}