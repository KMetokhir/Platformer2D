using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rigidbody2D), typeof(PlayerView))]
[RequireComponent(typeof(CollisionChecker))]
public class Player : MonoBehaviour
{
    [SerializeField] InputReader _input;
    [SerializeField] Wallet _wallet;

    private Mover _mover;
    private PlayerView _view;
    private CollisionChecker _collisionChecker;   
    

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        _view = GetComponent<PlayerView>();

        _mover.Init(rigidbody);

        _collisionChecker = GetComponent<CollisionChecker>();       
    }

    private void OnEnable()
    {
        _mover.FacingChanged += OnFacingchanged;
        _mover.StartsMoving += OnStartsMoving;
        _mover.StopedMoving += OnStopedMoving;
        _mover.GroundStatusChanged += OnGroundStatusChanged;

        _collisionChecker.CollectableCollision += OnCollectableCollision;     

        _input.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
        _input.JumpButtonPressed += OnJumpButtonPressed;
    }

    private void OnDisable()
    {
        _mover.FacingChanged -= OnFacingchanged;
        _mover.StartsMoving -= OnStartsMoving;
        _mover.StopedMoving -= OnStopedMoving;
        _mover.GroundStatusChanged -= OnGroundStatusChanged;

        _collisionChecker.CollectableCollision -= OnCollectableCollision;

        _input.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;
        _input.JumpButtonPressed -= OnJumpButtonPressed;
    }

    private void OnJumpButtonPressed()
    {
        _mover.SetJump();
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

    private void OnGroundStatusChanged(bool isGrounded)
    {
        _view.SetJumpStatus(isGrounded);
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