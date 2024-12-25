using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private float _resourceSelectionRadius;
    [SerializeField] private Bag _bag;

    private Base _base;

    public void Init(Base @base)
    {
        _base = @base;
    }

    public void ExtractResource(Transform resource)
    {
        _mover.MoveToTarget(resource);
        _mover.Came += FindResourceAndTakeItBase;
    }

    private void FindResourceAndTakeItBase()
    {
        _mover.Came -= FindResourceAndTakeItBase;

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

        if (_bag.TryGetResource(out Resource resource))
            _base.TakeResource(this, resource);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _resourceSelectionRadius);
    }
}