using UnityEngine;

public class PatrolState : EnemyState
{
    private const float RightHorizontalDirection = 1;
    private const float LeftHorizontalDirection = -1;

    [SerializeField] private DamageableDetector _detector;
    [SerializeField] private Vector2 _patrolAria;

    private void Update()
    {
        if (TryChangeDirection(_patrolAria.x, _patrolAria.y))
        {
            SetEyeDirection();
        }
    }

    public override void Enter()
    {
        base.Enter();

        _detector.DamageableDetected += OnDamageableFound;
        Move(RightHorizontalDirection);
        SetEyeDirection();
    }

    public override void Exit()
    {
        _detector.DamageableDetected -= OnDamageableFound;

        base.Exit();
    }

    private void SetEyeDirection()
    {
        Vector2 direction = new Vector2(CurrentHorizontalDirection, 0);
        _detector.SetEyeDirection(direction);
    }

    private bool TryChangeDirection(float leftBound, float rightBound)
    {
        if (TryGetHorizontalDirection(leftBound, rightBound, out float direction))
        {
            if (CurrentHorizontalDirection != direction)
            {
                Move(direction);

                return true;
            }
        }

        return false;
    }

    private bool TryGetHorizontalDirection(float leftBound, float rightBound, out float horizontalDirection)
    {
        bool isSuccess = false;
        horizontalDirection = 0;

        if (transform.position.x >= rightBound)
        {
            horizontalDirection = LeftHorizontalDirection;
            isSuccess = true;
        }
        else if (transform.position.x <= leftBound)
        {
            horizontalDirection = RightHorizontalDirection;
            isSuccess = true;
        }

        return isSuccess;
    }

    private void OnDamageableFound(IDamageable damageable)
    {
        TransitToTarGetState();
    }
}