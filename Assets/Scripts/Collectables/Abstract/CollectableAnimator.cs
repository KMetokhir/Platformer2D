using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CollectableAnimator : MonoBehaviour
{
    private const string DestroyVariable = "IsDestroy";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayDestroyAnimation()
    {
        _animator.SetBool(DestroyVariable, true);
    }
}