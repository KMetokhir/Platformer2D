using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AbstractMovableView : MonoBehaviour
{
    private readonly int IsRuning = Animator.StringToHash(nameof(IsRuning));
    private readonly int Hit = Animator.StringToHash(nameof(Hit));

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
}