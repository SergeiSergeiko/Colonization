using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ResetRigidbodyOnDespawn : MonoBehaviour, IDespawnable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void IDespawnable.OnDespawn()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}