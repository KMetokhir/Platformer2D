using UnityEngine;

[RequireComponent(typeof(Mover), typeof(PatrolBehaviour), typeof(DamageableDetector))]
[RequireComponent(typeof(Rotator))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Vector2 _patrolAria;
    [SerializeField] private Health _health;

    private DamageableDetector _detector;
    private PatrolBehaviour _patrolBehaviour;
    private ChaseBehaviour _chaseBehaviour;

    private Mover _mover;
    private Attacker _attacker;
    private Rotator _rotator;

    private EnemyView _view;

    public Transform Transform => transform;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _detector = GetComponent<DamageableDetector>();
        _rotator = GetComponent<Rotator>();

        _patrolBehaviour = GetComponent<PatrolBehaviour>();
        _patrolBehaviour.Init(_patrolAria, _mover, _detector);

        _chaseBehaviour = GetComponent<ChaseBehaviour>();

        _view = GetComponent<EnemyView>();
    }

    private void OnEnable()
    {
        _mover.FacingChanged += OnFacingchanged;
        _mover.StartsMoving += OnStartsMoving;
        _mover.StopedMoving += OnStopedMoving;

        _detector.DamageableDetected += OnDamageableFound;
        _detector.DamageableLost += OnDamageableLost;

        _attacker.AttackPerforming += OnAtackPerforming;
    }

    private void Start()
    {
        _patrolBehaviour.Enter();
    }

    private void OnDisable()
    {
        _mover.FacingChanged -= OnFacingchanged;
        _mover.StartsMoving -= OnStartsMoving;
        _mover.StopedMoving -= OnStopedMoving;

        _detector.DamageableDetected -= OnDamageableFound;
        _detector.DamageableLost -= OnDamageableLost;

        _attacker.AttackPerforming -= OnAtackPerforming;
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

    private void OnDamageableFound(IDamageable damageable)
    {
        _patrolBehaviour.Exit();

        _chaseBehaviour.Init(damageable.Transform, _mover, _detector, _attacker);
        _chaseBehaviour.Enter();
    }

    private void OnDamageableLost()
    {
        _chaseBehaviour.Exit();
        _patrolBehaviour.Enter();
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