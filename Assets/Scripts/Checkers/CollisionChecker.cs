using System;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public event Action<ICollectable> CollectableCollided;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            CollectableCollided.Invoke(collectable);
        }
    }
}