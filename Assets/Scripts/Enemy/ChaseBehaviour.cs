using UnityEngine;

public class ChaseBehaviour : AbstractMoveInBoundsBehaviour
{
    [SerializeField] private float _attackDistance;
    [SerializeField] private uint _damage;
    [SerializeField] private float _attackDelay;

    private bool _isInited = false;

    private Transform _target;
    private DamageableDetector _detector;
    private Attacker _attacker;

    private void Update()
    {
        if (_isInited == false || IsActive == false)
        {
            return;
        }

        LookAtTarget();

        TryChangeDirection(_target.position.x, _target.position.x);

        if (_attacker.IsAtacking)
        {
            float stopDirection = 0;
            Move(stopDirection);
        }
    }

    public void Init(Transform target, Transform behaviourOwner, Mover mover, DamageableDetector detector, Attacker attacker)
    {
        Init(behaviourOwner, mover);

        if (_isInited)
        {
            return;
        }

        _isInited = true;

        _target = target;
        _detector = detector;
        _attacker = attacker;
    }

    public override void Enter()
    {
        if (_isInited == false || IsActive)
        {
            return;
        }

        base.Enter();

        _detector.SqrDistanceChanged += OnSqrDistanceChanged;
    }

    public override void Exit()
    {
        base.Exit();
        _isInited = false;

        _detector.SqrDistanceChanged -= OnSqrDistanceChanged;
    }

    private void OnSqrDistanceChanged(IDamageable damageable, float sqrDistance)
    {
        if (sqrDistance <= _attackDistance * _attackDistance)
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

    private void LookAtTarget()
    {
        Vector2 direction = _target.position - Position;
        _detector.SetEyeDirection(direction);
    }
}