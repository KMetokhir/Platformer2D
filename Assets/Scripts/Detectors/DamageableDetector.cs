using System;
using System.Collections;
using UnityEngine;

public class DamageableDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _rayCastLayers;
    [SerializeField] private Transform _eyePoint;
    [SerializeField] private float _detectDistance;
    [SerializeField] private float _detectInterval;

    private float _sqrDistanceToTarget = 0;
    private Vector3 _eyeDirection;
    private IDamageable _currentDemageable;

    private Coroutine _detectCoroutine;

    private bool _isWork = false;

    public event Action<IDamageable> DamageableDetected;
    public event Action DamageableLost;
    public event Action<IDamageable, float> SqrDistanceChanged;

    public Vector2 DamageablePosition => _currentDemageable.Position;

    private void Start()
    {
        StartDetecting();
    }

    private void OnDisable()
    {
        StopDetecting();
    }

    private void StartDetecting()
    {
        if (_isWork || _detectCoroutine != null)
        {
            return;
        }

        _isWork = true;
        _detectCoroutine = StartCoroutine(Detect());
    }

    private void StopDetecting()
    {
        _isWork = false;
        _currentDemageable = null;
        _sqrDistanceToTarget = 0;

        StopCoroutine(_detectCoroutine);
        _detectCoroutine = null;
    }

    public void SetEyeDirection(Vector3 direction)
    {
        _eyeDirection = direction;
    }

    private IEnumerator Detect()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_detectInterval);

        while (_isWork)
        {
            if (_currentDemageable != null)
            {
                float sqrDistance = GetSqrDistance(_eyePoint.position, _currentDemageable.Position);

                if (_sqrDistanceToTarget != sqrDistance)
                {
                    _sqrDistanceToTarget = sqrDistance;
                    SqrDistanceChanged?.Invoke(_currentDemageable, _sqrDistanceToTarget);
                }
            }

            if (TryGetDamageable(out IDamageable damegeable))
            {
                if (_currentDemageable != damegeable)
                {

                    _currentDemageable = damegeable;
                    DamageableDetected?.Invoke(_currentDemageable);
                }
            }
            else if (_currentDemageable != null)
            {
                DamageableLost?.Invoke();

                _currentDemageable = null;
            }

            yield return waitingTime;
        }
    }

    private float GetSqrDistance(Vector3 start, Vector3 end)
    {
        return (end - start).sqrMagnitude;
    }

    private bool TryGetDamageable(out IDamageable damageable)
    {
        bool isDetected = false;
        damageable = null;

        Ray2D ray = new Ray2D(transform.position, _eyeDirection * _detectDistance);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _eyeDirection, _detectDistance, _rayCastLayers);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out damageable))
            {
                isDetected = true;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * _detectDistance, Color.green);

        return isDetected;
    }
}