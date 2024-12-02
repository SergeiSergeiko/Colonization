using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private float _resourceSelectionRadius;

    private Transform _base;

    public bool IsBusy { get; private set; }

    public void GoToGetResource(Transform resource)
    {
        IsBusy = true;
        _mover.MoveToTarget(resource);
        _mover.Came += FindResourceAndTakeItBase;
    }

    private void FindResourceAndTakeItBase()
    {
        _mover.Came -= FindResourceAndTakeItBase;

        PickUpResource();
        _mover.MoveToTarget(_base);
    }

    private void PickUpResource()
    {
        Collider[] colliders =
            Physics.OverlapSphere(transform.position, _resourceSelectionRadius);

        foreach (Collider collider in colliders)
            if (collider.TryGetComponent(out Resourse resource))
                resource.transform.SetParent(transform);
    }

    private void MoveToBase()
    {
        _mover.MoveToTarget(_base);
        _mover.Came += OnCameToBase;
    }

    private void OnCameToBase()
    {
        _mover.Came -= OnCameToBase;
        IsBusy = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, _resourceSelectionRadius);
    }
}