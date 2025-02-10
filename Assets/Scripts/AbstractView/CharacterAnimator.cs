using UnityEngine;

[RequireComponent(typeof(Animator))]
public  class CharacterAnimator : MonoBehaviour
{
    private readonly int _isRuning = Animator.StringToHash("IsRuning");
    private readonly int _hit = Animator.StringToHash("Hit");

    private Animator _animator;

    protected Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayRunAnimation()
    {       
        _animator.SetBool(_isRuning, true);
    }

    public void PlayIdleAnimation()
    {
        _animator.SetBool(_isRuning, false);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_hit);
    }
}