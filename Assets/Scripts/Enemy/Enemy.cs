using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rigidbody2D), typeof(PatrolLogic))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 _patrolAria;

    private PatrolLogic _patrolLogic;
    private Mover _mover;

    private EnemyView _view;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        _mover.Init(rigidbody);

        _patrolLogic = GetComponent<PatrolLogic>();
        _patrolLogic.Init(_patrolAria, transform);

        _view = GetComponent<EnemyView>();
    }

    private void OnEnable()
    {
        _patrolLogic.HorizontalDirectionChanged += OnHorizontalDirectionChanged;

        _mover.FacingChanged += OnFacingchanged;
        _mover.StartsMoving += OnStartsMoving;
    }

    private void Start()
    {
        _patrolLogic.StartPatrol();
    }

    private void OnDisable()
    {
        _patrolLogic.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;

        _mover.FacingChanged -= OnFacingchanged;
        _mover.StartsMoving -= OnStartsMoving;
    }

    private void OnHorizontalDirectionChanged(float direction)
    {
        _mover.SetHorizontalDirection(direction);
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