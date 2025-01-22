using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class VampireAria : MonoBehaviour
{
    [SerializeField] private float _radius;

    private CircleCollider2D _collider;
    private SpriteRenderer _renderer;

    public event Action<IVampireTarget> TargetDetected;
    public event Action<IVampireTarget> TargetLost;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector2(_radius, _radius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IVampireTarget vampireTarget))
        {           
            TargetDetected?.Invoke(vampireTarget);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {       
        if (other.TryGetComponent(out IVampireTarget vampireTarget))
        {
            TargetLost?.Invoke(vampireTarget);
        }
    }

    public void Activate()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
    }

    public void Deactivate()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
    }
}