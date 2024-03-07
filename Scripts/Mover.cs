using UnityEngine;

public class Mover : MonoBehaviour
{
    private Vector3 _movementDirection;

    public void SetMoveDirection(Vector3 direction)
    {
        _movementDirection = direction.normalized;
    }

    private void Update()
    {
        transform.Translate(_movementDirection * Time.deltaTime, Space.World);
    }
}
