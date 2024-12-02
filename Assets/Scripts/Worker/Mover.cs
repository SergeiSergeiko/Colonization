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

    private void Update()
    {
        if (_target != null)
            Move();
    }

    public void MoveToTarget(Transform target)
    {
        _target = target;
    }

    private void Move()
    {
        _rigidbody.transform.position = Vector3.MoveTowards(transform.position,
            _target.position, _speed * Time.deltaTime);

        if (IsCame())
            EndToMove();
    }

    private bool IsCame()
    {
        float distance = 0.5f;

        return Vector3.Distance(transform.position, _target.position) < distance;
    }

    private void EndToMove()
    {
        _target = null;
        Came?.Invoke();
    }
}