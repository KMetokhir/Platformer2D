using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rigidbody2D), typeof(PlayerView))]
[RequireComponent(typeof(CollisionChecker), (typeof(GroundChecker)))]
[RequireComponent(typeof(Jumper), typeof(Rotator))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private InputReader _input;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Health _health;

    private Mover _mover;
    private Jumper _jumper;
    private PlayerView _view;
    private CollisionChecker _collisionChecker;
    private DamageableDetector _detector;
    private Attacker _attacker;
    private Rotator _rotator;

    public Transform Transform => transform;

    private void Awake()
    {
        _detector = GetComponent<DamageableDetector>();

        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _view = GetComponent<PlayerView>();
        _attacker = GetComponent<Attacker>();
        _rotator = GetComponent<Rotator>();

        _collisionChecker = GetComponent<CollisionChecker>();
    }

    private void OnEnable()
    {
        _mover.FacingChanged += OnFacingchanged;
        _mover.StartsMoving += OnStartsMoving;
        _mover.StopedMoving += OnStopedMoving;

        _jumper.JumpStart += OnJumpStart;
        _jumper.JumpEnd += OnJumpEnd;

        _collisionChecker.CollectableCollision += OnCollectableCollision;

        _input.HorizontalDirectionChanged += OnInputHorizontalDirectionChanged;
        _input.JumpButtonPressed += OnJumpButtonPressed;

        _detector.DamageableDetected += OnDamageableFound;
        _detector.DamageableLost += OnDamageableLost;

        _attacker.AttackPerforming += OnAttackPerforming;
    }

    private void OnDisable()
    {
        _mover.FacingChanged -= OnFacingchanged;
        _mover.StartsMoving -= OnStartsMoving;
        _mover.StopedMoving -= OnStopedMoving;

        _jumper.JumpStart -= OnJumpStart;
        _jumper.JumpEnd -= OnJumpEnd;

        _collisionChecker.CollectableCollision -= OnCollectableCollision;

        _input.HorizontalDirectionChanged -= OnInputHorizontalDirectionChanged;
        _input.JumpButtonPressed -= OnJumpButtonPressed;

        _detector.DamageableDetected -= OnDamageableFound;
        _detector.DamageableLost -= OnDamageableLost;

        _attacker.AttackPerforming -= OnAttackPerforming;
    }

    public void TakeDamage(uint value)
    {
        _health.Decrease(value);
    }

    private void OnAttackPerforming()
    {
        _view.PlayAttackAnimation();
    }

    private void OnJumpStart()
    {
        _view.PlayJump();
    }

    private void OnJumpEnd()
    {
        _view.StopPlayingJump();
    }

    private void OnDamageableFound(IDamageable damageable)
    {
        _attacker.StartAttack(damageable);
    }

    private void OnDamageableLost()
    {
        _attacker.StopAttack();
    }

    private void OnJumpButtonPressed()
    {
        _jumper.SetJump();
    }

    private void OnInputHorizontalDirectionChanged(float horizontalDirection)
    {
        _mover.SetHorizontalDirection(horizontalDirection);
    }

    private void OnCollectableCollision(ICollectable collectable)
    {
        switch (collectable)
        {
            case Coin:
                {
                    _wallet.IncreaseMoney(collectable.Collect());
                    break;
                }
            case FirstAid:
                {
                    _health.Increase(collectable.Collect());
                    break;
                }
        }
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

        Vector2 direction = new Vector2(facingDirection, 0);

        _detector.SetEyeDirection(direction);
    }
}