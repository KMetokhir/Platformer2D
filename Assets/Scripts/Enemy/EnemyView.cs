using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private const string RunBoolVariable = "IsRuning"; 

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
