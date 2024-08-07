using System;
using UnityEngine;

public abstract class Collectable : MonoBehaviour, ICollectable
{
    [SerializeField] private uint _value;

    public event Action Collected;

    public uint Collect()
    {
        Collected?.Invoke();

        return _value;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}