using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private Vector3 _target;
    private bool _hasTarget;

    public event Action Came;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_hasTarget)
            Move();
    }

    private void Update()
    {
        if (_hasTarget)
        {
            LookAtMoveDirection();

            if (IsCame())
                EndToMove();
        }
    }

    public void MoveToTarget(Vector3 target)
    {
        _target = target;
        _hasTarget = true;
    }

    private void Move()
    {
        _rigidbody.transform.position = Vector3.MoveTowards(transform.position,
            _target, _speed * Time.deltaTime);
    }

    private void LookAtMoveDirection()
    {
        Vector3 correctPosition = new(_target.x, transform.position.y, _target.z);

        transform.LookAt(correctPosition);
    }

    private bool IsCame()
    {
        float distance = 0.5f;
        
        return (_target - transform.position).sqrMagnitude < distance * distance;
    }

    private void EndToMove()
    {
        _hasTarget = false;
        Came?.Invoke();
    }
}