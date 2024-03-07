using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Rigidbody[] _enemyPrefabs;
    [SerializeField] private Transform[] _targets;
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private bool _canSpawn = true;

    private Transform[] _spawnTarget;
    private int[] _enemyPrefabIndex;
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
        _enemyPrefabIndex = new int[_spawnPoints.Length];

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnTarget[i] = _targets[i % _targets.Length];
            _enemyPrefabIndex[i] = i % _enemyPrefabs.Length;
        }
    }

    private void Spawn()
    {
        int randomSpawnIndex = Random.Range(0, _spawnPoints.Length);

        Transform spawnPoint = _spawnPoints[randomSpawnIndex];
        Rigidbody enemyPrefab = _enemyPrefabs[_enemyPrefabIndex[randomSpawnIndex]];
        Rigidbody enemyObject = Instantiate(enemyPrefab);
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
