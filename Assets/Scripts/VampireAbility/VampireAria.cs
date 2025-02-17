using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class VampireAria : MonoBehaviour
{
    [SerializeField] private float _radius;

    private CircleCollider2D _collider;
    private SpriteRenderer _renderer;

    private List<IVampireTarget> _targets;

    public event Action<IVampireTarget> TargetEntered;
    public event Action<IVampireTarget> AllTargetsLost;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector2(_radius, _radius);

        _targets = new List<IVampireTarget>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IVampireTarget vampireTarget))
        {
            if (_targets.Contains(vampireTarget) == false)
            {
                _targets.Add(vampireTarget);
                TargetEntered?.Invoke(vampireTarget);
            }
            else
            {
                throw new Exception("Incorrect triger enter");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IVampireTarget vampireTarget))
        {
            if (_targets.Contains(vampireTarget))
            {
                _targets.Remove(vampireTarget);
            }
            else
            {
                throw new Exception("Incorrect triger exit");
            }

            if (_targets.Count == 0)
            {
                AllTargetsLost?.Invoke(vampireTarget);
            }
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

    public bool TryGetClosestTarget(out IVampireTarget target)
    {
        target = null;

        if (_targets.Count != 0)
        {
            target = _targets.OrderBy(t => GetSqrDistance(t.Position, transform.position)).First();
        }

        return target != null;
    }

    private float GetSqrDistance(Vector3 start, Vector3 end)
    {
        return (end - start).sqrMagnitude;
    }
}