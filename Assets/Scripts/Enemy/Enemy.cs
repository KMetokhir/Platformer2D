using UnityEngine;

[RequireComponent(typeof(Mover), typeof(DamageableDetector))]
[RequireComponent(typeof(Rotator))]
public class Enemy : MonoBehaviour, IDamageable, IVampireTarget
{
    [SerializeField] private Health _health;

    private Mover _mover;
    private Attacker _attacker;
    private Rotator _rotator;

    private EnemyView _view;

    public Transform Transform => transform;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _rotator = GetComponent<Rotator>();

        _view = GetComponent<EnemyView>();
    }

    private void OnEnable()
    {
        _mover.FacingChanged += OnFacingchanged;
        _mover.StartsMoving += OnStartsMoving;
        _mover.StopedMoving += OnStopedMoving;

        _attacker.AttackPerforming += OnAtackPerforming;
    }

    private void OnDisable()
    {
        _mover.FacingChanged -= OnFacingchanged;
        _mover.StartsMoving -= OnStartsMoving;
        _mover.StopedMoving -= OnStopedMoving;

        _attacker.AttackPerforming -= OnAtackPerforming;
    }

    public uint Suck(uint value)
    {
        _view.Blink();

        return _health.Decrease(value);
    }

    public void TakeDamage(uint value)
    {
        _health.Decrease(value);
    }

    private void OnAtackPerforming()
    {
        _view.PlayAttackAnimation();
    }

    private void OnStopedMoving()
    {
        _view.PlayIdleAnimation();
    }

    private void OnStartsMoving()
    {
        _view.PlayRunAnimation();
    }

    private void OnFacingchanged(float facingDirection)
    {
        _rotator.Flip(facingDirection);
    }
}