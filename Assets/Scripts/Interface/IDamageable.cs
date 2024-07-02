using UnityEngine;

public interface IDamageable
{
    public Transform Transform { get; }
    public void TakeDamage(uint value);
}