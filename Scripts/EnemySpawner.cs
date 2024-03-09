using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnPoint[] _spawnPoints;
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private bool _canSpawn = true;

    private WaitForSeconds _spawnWait;

    private void Start()
    {
        _spawnWait = new WaitForSeconds(_timeToSpawn);
        StartCoroutine(Spawner());
    }

    private void Spawn()
    {
        int randomSpawnIndex = Random.Range(0, _spawnPoints.Length);

        EnemySpawnPoint spawnPoint = _spawnPoints[randomSpawnIndex];
        spawnPoint.Spawn();
    }

    private IEnumerator Spawner()
    {
        while (_canSpawn)
        {
            Spawn();
            yield return _spawnWait;
        }
    }
}
