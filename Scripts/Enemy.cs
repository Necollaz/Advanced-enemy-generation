using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;

    private void Update()
    {
        if(_target != null)
        {
            MoveTowardsTarget();
            RotateTowardsPlayer();
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void MoveTowardsTarget()
    {
        if(_target != null)
        {
            Vector3 direction = _target.position - transform.position;
            transform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
        }
    }

    private void RotateTowardsPlayer()
    {
        if(_target != null)
        {
            transform.LookAt(_target);
        }
    }
}
