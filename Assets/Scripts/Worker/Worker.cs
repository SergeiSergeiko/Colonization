using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private float _resourceSelectionRadius;
    [SerializeField] private Bag _bag;

    private Base _base;

    public bool IsBusy { get; private set; } = false;

    public void Init(Base @base)
    {
        _base = @base;
    }

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
        MoveToBase();
    }

    private void PickUpResource()
    {
        Collider[] colliders =
            Physics.OverlapSphere(transform.position, _resourceSelectionRadius);

        foreach (Collider collider in colliders)
            if (collider.TryGetComponent(out Resource resource))
                _bag.TakeResource(resource);
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
            _base.TakeResource(resource);

        IsBusy = false;
    }
}