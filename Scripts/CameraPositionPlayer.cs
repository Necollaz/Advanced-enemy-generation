using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;

    private Transform _target;

    private void Start()
    {
        _target = FindFirstObjectByType<Player>().transform;
    }

    private void Update()
    {
        Vector3 newPosition = _target.transform.position + _offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, _speed * Time.deltaTime);
    }
}
