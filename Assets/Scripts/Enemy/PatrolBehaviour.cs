using UnityEngine;

public class PatrolBehaviour : AbstractMoveInBoundsBehaviour
{
    private Vector2 _patrolAria;   
    private DamageableDetector _detector;

    private bool _isInited = false;   

    private void Update()
    {
        if (_isInited == false || base.IsActive == false)
        {
            return;
        }

        if (TryChangeDirection( _patrolAria.x, _patrolAria.y))
        {
            SetEyeDirection();
        }      
    }

    public override void Enter()
    {
        if (_isInited == false || base.IsActive)
        {
            return;
        }

        base.Enter();

        Move(RightHorizontalDirection);

        SetEyeDirection();
    }

    public override void Exit()
    {
        base.Exit();        
    }

    public void Init(Vector2 patrolAria, Transform behaviourOwner, Mover mover, DamageableDetector detector)
    {
        Init(behaviourOwner, mover);

        if (_isInited)
        {
            return;
        }

        _isInited = true;

        _patrolAria = patrolAria;             
        _detector = detector;
    }  

    private void SetEyeDirection()
    {
        Vector2 direction = new Vector2(CurrentHorizontalDirection, 0); 
        _detector.SetEyeDirection(direction);
    }
}