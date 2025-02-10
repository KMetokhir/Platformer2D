using UnityEngine;

public class ChaseState : EnemyState
{
    [SerializeField] private DamageableDetector _detector;
    [SerializeField] private Attacker _attacker;

    private void Update()
    {
        LookAtTarget(_detector.DamageablePosition);

        float horizontalDirection = (_detector.DamageablePosition.x - transform.position.x) / Mathf.Abs((_detector.DamageablePosition.x - transform.position.x));

        if (horizontalDirection != CurrentHorizontalDirection)
        {
            Move(horizontalDirection);
        }

        if (_attacker.IsAtacking)
        {
            float stopDirection = 0;
            Move(stopDirection);
        }
    }

    public override void Enter()
    {
        if (enabled == true)
        {
            return;
        }

        base.Enter();

        _detector.SqrDistanceChanged += OnSqrDistanceChanged;
    }

    public override void Exit()
    {
        base.Exit();

        _detector.SqrDistanceChanged -= OnSqrDistanceChanged;
    }

    private void OnSqrDistanceChanged(IDamageable damageable, float sqrDistance)
    {
        if (sqrDistance <= _attacker.AttackDistance * _attacker.AttackDistance)
        {
            if (_attacker.IsAtacking == false)
            {
                _attacker.StartAttack(damageable);
            }
        }
        else
        {
            _attacker.StopAttack();
        }
    }

    private void LookAtTarget(Vector3 target)
    {
        Vector2 direction = target - transform.position;
        _detector.SetEyeDirection(direction);
    }
}