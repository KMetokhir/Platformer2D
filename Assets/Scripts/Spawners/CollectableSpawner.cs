using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private Collectable _collectablePrefab;
    [SerializeField] private List<Transform> _spawnPosition;

    private void Awake()
    {
        SpawnAll();
    }

    public void SpawnAll()
    {
        foreach (Transform spawnpoint in _spawnPosition)
        {
            Spawn(spawnpoint.position);
        }
    }

    private void Spawn(Vector3 position)
    {
        Instantiate(_collectablePrefab, position, Quaternion.identity);
    }
}