using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groungCheckerRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded => Physics2D.OverlapCircle(_groundCheck.position, _groungCheckerRadius, groundLayer);    
}
