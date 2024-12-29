using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private Transform _target;

    public event Action Came;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
            Move();
    }

    private void Update()
    {
        if (_target != null)
        {
            LookAtMoveDirection();

            if (IsCame())
                EndToMove();
        }
    }

    public void MoveToTarget(Transform target)
    {
        _target = target; 
    }

    private void Move()
    {
        _rigidbody.transform.position = Vector3.MoveTowards(transform.position,
            _target.position, _speed * Time.deltaTime);
    }

    private void LookAtMoveDirection()
    {
        Vector3 correctPosition = 
            new(_target.position.x, transform.position.y, _target.position.z);

        transform.LookAt(correctPosition);
    }

    private bool IsCame()
    {
        float distance = 0.5f;
        
        return (_target.position - transform.position).sqrMagnitude < distance * distance;
    }

    private void EndToMove()
    {
        _target = null;
        Came?.Invoke();
    }
}