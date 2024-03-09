using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Rigidbody _prefabEnemy;
    [SerializeField] private Transform _target;

    public void Spawn()
    {
        Rigidbody enemyObject = Instantiate(_prefabEnemy);
        enemyObject.transform.position = transform.position;

        Enemy enemyComponent = enemyObject.GetComponent<Enemy>();
        enemyComponent.SetTarget(_target);
    }
}
