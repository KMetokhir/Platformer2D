using UnityEngine;

public class PlayerView : CharacterAnimator
{
    public void PlayJump()
    {
        bool isJumping = true;
        Animator.SetBool(AnimatorParameters.IsJumping, isJumping);
    }

    public void StopPlayingJump()
    {
        bool isJumping = false;
        Animator.SetBool(AnimatorParameters.IsJumping, isJumping);
    }
}