using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    protected Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayRunAnimation()
    {
        _animator.SetBool(AnimatorParameters.IsRuning, true);
    }

    public void PlayIdleAnimation()
    {
        _animator.SetBool(AnimatorParameters.IsRuning, false);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(AnimatorParameters.Hit);
    }
}