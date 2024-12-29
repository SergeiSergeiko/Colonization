using System;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private float _resourceSelectionRadius;
    [SerializeField] private Bag _bag;

    private Base _base;

    public event Action<Worker> Freed;

    public bool IsBusy { get; private set; } = false;

    public void Init(Base @base)
    {
        _base = @base;
    }

    public void ExtractResource(Transform resource)
    {
        IsBusy = true;
        _mover.MoveToTarget(resource);
        _mover.Came += OnCameToResource;
    }

    private void OnCameToResource()
    {
        _mover.Came -= OnCameToResource;

        PickUpResource();
        MoveToBase();
    }

    private void PickUpResource()
    {
        Collider[] colliders =
            Physics.OverlapSphere(transform.position, _resourceSelectionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Resource resource))
            {
                _bag.TakeResource(resource);

                break;
            }
        }
    }

    private void MoveToBase()
    {
        _mover.MoveToTarget(_base.transform);
        _mover.Came += OnCameToBase;
    }

    private void OnCameToBase()
    {
        _mover.Came -= OnCameToBase;

        if (_bag.TryGiveResource(out Resource resource))
            _base.TakeResource(this, resource);

        IsBusy = false;
        Freed?.Invoke(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _resourceSelectionRadius);
    }
}