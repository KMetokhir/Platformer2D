using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string JumpButton = "Jump";
    private const string HorizontalAxis = "Horizontal";

    private float _lastHorizontalDirection;

    public event Action<float> HorizontalDirectionChanged;
    public event Action JumpButtonPressed;

    private void Update()
    {
        float horizontalDirection = Input.GetAxisRaw(HorizontalAxis);

        if (_lastHorizontalDirection != horizontalDirection)
        {
            HorizontalDirectionChanged?.Invoke(horizontalDirection);
            _lastHorizontalDirection = horizontalDirection;
        }

        if (Input.GetButtonDown(JumpButton))
        {
            JumpButtonPressed?.Invoke();
        }
    }
}
