using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
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
        Instantiate(_coinPrefab, position, Quaternion.identity);
    }
}