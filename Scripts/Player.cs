using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _reloadTime;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool _canHit = true;
    private bool _isRunning = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        SetAnimations();
    }

    private void Move()
    {
        float verticalDirection = Input.GetAxis(Vertical);
        float horizontalDirection = Input.GetAxis(Horizontal);
        Vector3 movement = new Vector3(horizontalDirection, 0f, verticalDirection).normalized * _moveSpeed * Time.deltaTime;

        if(Input.GetKey(KeyCode.LeftShift) && verticalDirection > 0)
        {
            _isRunning = true;
            movement *= _runSpeedMultiplier;
        }
        else
        {
            _isRunning = false;
        }

        _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);
        transform.Translate(movement, Space.World);

        UpdateAnimationParameters(movement);
    }

    private void UpdateAnimationParameters(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void SetAnimations()
    {
        if (_isRunning)
        {
            _animator.SetBool("isRun", true);
            _animator.SetBool("isWalk", false);
        }
        else if(_rigidbody.velocity.magnitude > 0)
        {
            _animator.SetBool("isWalk", true);
            _animator.SetBool("isRun", false);
        }
        else
        {
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isRun", false);
        }

        if (Input.GetMouseButton(0) && _canHit)
        {
            _animator.SetTrigger("isAttack");
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        _canHit = false;
        yield return new WaitForSeconds(_reloadTime);
        _canHit = true;
    }
}
