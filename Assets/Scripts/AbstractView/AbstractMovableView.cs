using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AbstractMovableView : MonoBehaviour
{
    public readonly int IsRuning = Animator.StringToHash(nameof(IsRuning));    
    public readonly int Hit = Animator.StringToHash(nameof(Hit));

    private bool _isFacingRight = true;
    private Animator _animator;

    protected Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayRunAnimation()
    {
        _animator.SetBool(IsRuning, true);
    }

    public void PlayIdleAnimation()
    {
        _animator.SetBool(IsRuning, false);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(Hit);
    }

    public void Flip(float facingDirection)
    {
        if (_isFacingRight && facingDirection < 0f || !_isFacingRight && facingDirection > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}