using System;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Bag _bag;
    [SerializeField] private Base _prefab;
    [SerializeField] private float _resourceSelectionRadius;

    private PickUper _pickUper = new();
    private Builder _builder;
    private Scanner _scanner;
    private MouseInput _mouseInput;
    private Base _base;
    private bool _isBusy;

    public event Action<Worker> Freed;

    public bool IsBusy
    {
        get
        {
            return _isBusy;
        }

        private set
        {
            _isBusy = value;

            if (_isBusy == false)
                Freed?.Invoke(this);
        }
    }

    public void Init(Base @base, Builder builder, Scanner scanner, MouseInput mouseInput)
    {
        _base = @base;
        _builder = builder;
        _scanner = scanner;
        _mouseInput = mouseInput;

        IsBusy = false;
    }

    public void BuildBase(Vector3 position)
    {
        IsBusy = true;
        _mover.MoveToTarget(position);
        _mover.Came += OnCameBuildBase;
    }

    public void ExtractResource(Vector3 position)
    {
        IsBusy = true;
        _mover.MoveToTarget(position);
        _mover.Came += OnCameToResource;
    }

    private void OnCameBuildBase()
    {
        _mover.Came -= OnCameBuildBase;

        Base @base = _builder.Build(_prefab, transform.position) as Base;
        @base.Init(_builder, _scanner, _mouseInput);

        RemoveYourselfFromBase();
        ReInit(@base);
        @base.AddWorker(this);

        IsBusy = false;
    }

    private void OnCameToResource()
    {
        _mover.Came -= OnCameToResource;

        Resource resource =
            _pickUper.PickUpResource(transform.position, _resourceSelectionRadius);
        if (resource != null)
            _bag.TakeResource(resource);

        MoveToBase();
    }

    private void MoveToBase()
    {
        _mover.MoveToTarget(_base.transform.position);
        _mover.Came += OnCameToBase;
    }

    private void OnCameToBase()
    {
        _mover.Came -= OnCameToBase;

        if (_bag.TryGiveResource(out Resource resource))
            _base.TakeResource(resource);

        IsBusy = false;
        Freed?.Invoke(this);
    }

    private void ReInit(Base @base)
    {
        Init(@base, _builder, _scanner, _mouseInput);
    }

    private void RemoveYourselfFromBase()
    {
        _base.RemoveWorker(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _resourceSelectionRadius);
    }
}