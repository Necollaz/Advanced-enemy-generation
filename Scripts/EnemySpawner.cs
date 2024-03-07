using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Rigidbody[] _enemyPrefab;
    [SerializeField] private Transform[] _targets;
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private bool _canSpawn = true;

    private Transform[] _spawnTarget;
    private WaitForSeconds _spawnWait;
    private bool _isSpawning = false;

    private void Start()
    {
        InitializeSpawnTargets();
        _spawnWait = new WaitForSeconds(_timeToSpawn);
        StartCoroutine(Spawner());
    }

    private void InitializeSpawnTargets()
    {
        _spawnTarget = new Transform[_spawnPoints.Length];

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnTarget[i] = _targets[i % _targets.Length];
        }
    }

    private void Spawn()
    {
        int randomSpawnIndex = Random.Range(0, _spawnPoints.Length);
        int randomEnemyIndex = Random.Range(0, _enemyPrefab.Length);

        Transform spawnPoint = _spawnPoints[randomSpawnIndex];
        Rigidbody enemyObject = Instantiate(_enemyPrefab[randomEnemyIndex]);
        enemyObject.transform.position = spawnPoint.position;
        Transform target = _spawnTarget[randomSpawnIndex];
        Enemy enemyComponent = enemyObject.GetComponent<Enemy>();

        if(enemyComponent != null)
        {
            enemyComponent.SetTarget(target);
        }
    }

    private IEnumerator Spawner()
    {
        while (_canSpawn)
        {
            if (!_isSpawning)
            {
                _isSpawning = true;
                Spawn();
                yield return _spawnWait;
                _isSpawning = false;
            }
            else
            {
                yield return null;
            }
        }
    }
}
