using System;

public interface IChangeable
{
    public event Action<float> ValueChanged;

    public uint MaxValue { get; }
}