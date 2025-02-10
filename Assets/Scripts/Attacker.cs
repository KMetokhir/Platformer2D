using System;
using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _attackDistance;
    [SerializeField] private uint _damage;
    [SerializeField] private float _attackDelay;

    private Coroutine _attackCoroutine;

    public event Action AttackPerforming;

    public bool IsAtacking { get; private set; }
    public float AttackDistance => _attackDistance;

    private void Start()
    {
        IsAtacking = false;
    }

    public void StartAttack(IDamageable damageable)
    {
        IsAtacking = true;

        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(Attack(damageable));
        }
    }

    public void StopAttack()
    {
        IsAtacking = false;
    }

    private IEnumerator Attack(IDamageable damageable)
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_attackDelay);

        while (IsAtacking)
        {
            AttackPerforming?.Invoke();
            damageable.TakeDamage(_damage);
            yield return waitingTime;
        }

        _attackCoroutine = null;
    }
}