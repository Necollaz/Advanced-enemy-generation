using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private Transform _currentWaypoint;

    private void Start()
    {
        SetRandomWaypoint();
    }

    private void Update()
    {
        MoveToWaypoint();
    }

    private void SetRandomWaypoint()
    {
        int randomIndex = Random.Range(0, _waypoints.Length);
        _currentWaypoint = _waypoints[randomIndex];
    }

    private void MoveToWaypoint()
    {
        if(_currentWaypoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.position, _speed * Time.deltaTime);

            if(transform.position == _currentWaypoint.position)
            {
                SetRandomWaypoint();
            }
        }
    }
}
