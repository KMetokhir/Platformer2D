using System.Collections;
using UnityEngine;

public abstract class SmoothBarView : BarView
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

    protected override void OnValueChanged(float value)
    {
        _bar.maxValue = _changeable.MaxValue;

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
        while (_bar.value != targetValue)
        {
            _bar.value = Mathf.MoveTowards(_bar.value, targetValue, _fillSpeed);

            yield return null;
        }
    }
}