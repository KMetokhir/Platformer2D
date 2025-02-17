using UnityEngine;

public interface IDamageable
{
    public Vector2 Position { get; }

    public void TakeDamage(uint value);
}