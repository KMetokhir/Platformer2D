using UnityEngine;

public class PlayerView : CharacterAnimator
{
    private readonly int _isJumping = Animator.StringToHash("IsJumping");

    public void PlayJump()
    {
        bool isJumping = true;
        Animator.SetBool(_isJumping, isJumping);
    }

    public void StopPlayingJump()
    {
        bool isJumping = false;
        Animator.SetBool(_isJumping, isJumping);
    }
}