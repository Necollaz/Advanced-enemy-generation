using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _enemyObjects;
    [SerializeField] private Transform[] _targets;
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private bool _canSpawn = true;

    private Transform[] _spawnTarget;

    private void Start()
    {
        InitializeSpawnTargets();
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
        int randomEnemyIndex = Random.Range(0, _enemyObjects.Length);

        Transform spawnPoint = _spawnPoints[randomSpawnIndex];
        GameObject enemyObject = Instantiate(_enemyObjects[randomEnemyIndex]);
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
            yield return new WaitForSeconds(_timeToSpawn);
            Spawn();
        }
    }
}
