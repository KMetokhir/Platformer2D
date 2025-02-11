using UnityEngine;

public static class AnimatorParameters
{
    public static readonly int IsRuning = Animator.StringToHash(nameof(IsRuning));
    public static readonly int Hit = Animator.StringToHash(nameof(Hit));
    public static readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
}