using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private const string RunBoolVariable = "IsRuning";
    private const string HitTrigger = "Hit";

    private bool _isFacingRight = true;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayRunAnimation()
    {
        _animator.SetBool(RunBoolVariable, true);
    }

    public void PlayIdleAnimation()
    {
        _animator.SetBool(RunBoolVariable, false);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(HitTrigger);
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