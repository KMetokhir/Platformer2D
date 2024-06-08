using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rigidbody2D), typeof(PlayerView))]
[RequireComponent(typeof(CollisionChecker), (typeof(GroundChecker)))]
[RequireComponent(typeof(Jumper))]
public class Player : MonoBehaviour
{
    [SerializeField] InputReader _input;
    [SerializeField] Wallet _wallet;

    private Mover _mover;
    private Jumper _jumper;
    private PlayerView _view;
    private CollisionChecker _collisionChecker;
    private GroundChecker _groundChecker;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        _view = GetComponent<PlayerView>();
        _groundChecker = GetComponent<GroundChecker>();

        _mover.Init(rigidbody, _groundChecker);
        _jumper.Init(rigidbody, _groundChecker);

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

        _input.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
        _input.JumpButtonPressed += OnJumpButtonPressed;
    }

    private void OnDisable()
    {
        _mover.FacingChanged -= OnFacingchanged;
        _mover.StartsMoving -= OnStartsMoving;
        _mover.StopedMoving -= OnStopedMoving;       

        _jumper.JumpStart -= OnJumpStart;
        _jumper.JumpEnd -= OnJumpEnd;

        _collisionChecker.CollectableCollision -= OnCollectableCollision;

        _input.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;
        _input.JumpButtonPressed -= OnJumpButtonPressed;
    }

    private void OnJumpStart()
    {
        _view.SetJumpStatus(true);
    }

    private void OnJumpEnd()
    {
        _view.SetJumpStatus(false);
    }

    private void OnJumpButtonPressed()
    {
        _jumper.SetJump();
    }

    private void OnHorizontalDirectionChanged(float direction)
    {
        _mover.SetHorizontalDirection(direction);
    }

    private void OnCollectableCollision(ICollectable collectable)
    {
        if (collectable is Coin)
        {
            _wallet.IncreaseMoney(collectable.Collect());
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
        _view.Flip(facingDirection);
    }
}