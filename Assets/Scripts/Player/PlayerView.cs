using UnityEngine;

public class PlayerView : AbstractMovableView
{
    public readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));

    public void PlayJump()
    {
        bool isJumping = true;
        Animator.SetBool(IsJumping, isJumping);
    }

    public void StopPlayingJump()
    {
        bool isJumping = false;
        Animator.SetBool(IsJumping, isJumping);
    }
}