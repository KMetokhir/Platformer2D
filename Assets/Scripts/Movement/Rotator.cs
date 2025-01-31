using UnityEngine;

public class Rotator : MonoBehaviour
{
    private bool _isFacingRight = true;

    public void Flip(float facingDirection)
    {
        if (_isFacingRight && facingDirection < 0f || !_isFacingRight && facingDirection > 0f)
        {
            _isFacingRight = !_isFacingRight;

            float rotation = 180f;
            transform.rotation *= Quaternion.Euler(0, rotation, 0);
        }
    }
}