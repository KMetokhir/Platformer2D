using UnityEngine;

public interface IVampireTarget
{
    public Vector2 Position { get; }

    public uint Suck(uint value);
}