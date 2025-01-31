using System.Collections.Generic;
using UnityEngine;

public class Base : Building
{
    [Header("Components")]
    [SerializeField] private WorkersCoordinator _workersCoordinator;
    [SerializeField] private Storage _storage;

    [Header("Prefabs")]
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private Worker _workerPrefab;

    [Header("Settings")]
    [SerializeField] private int _buyWorkerPerResource;
    [SerializeField] private int _resourcesOnNewBase;
    [SerializeField] private int _minAmountWorkersForBuildNewBase;
    [SerializeField] private float _resourceExtractionRadius;

    private Builder _builder;
    private Scanner _scanner;
    private MouseInput _mouseInput;
    private WorkerSpawner _workerSpawner;

    private List<Worker> _workers = new();
    private Counter _counter;
    private Flag _flag;

    public Vector3 SpawnPoint => GetSpawnPositionWithScatter();

    private void Awake()
    {
        _counter = new Counter(_buyWorkerPerResource);
        _workerSpawner = new(_workerPrefab);

        _storage.ResourceReceived += OnResourceReceived;

        SetMiningMode();
    }

    private void OnDisable()
    {
        _mouseInput.LeftMouseButtonClicked -= OnLMBClicked;
        _scanner.ResourcesScanned -= StartMiningResources;
        _storage.ResourceReceived -= OnResourceReceived;
        
        ResetModes();
    }

    public void Init(Builder builder, Scanner scanner, MouseInput mouseInput, int startAmountWorkers = 0)
    {
        _builder = builder;
        _scanner = scanner;
        _mouseInput = mouseInput;

        _scanner.ResourcesScanned += StartMiningResources;
        _mouseInput.LeftMouseButtonClicked += OnLMBClicked;

        for (int i = 0; i < startAmountWorkers; i++)
            AddWorker(_workerSpawner.Spawn(SpawnPoint));
    }

    public void StartSetFlag()
    {
        if (_workers.Count <= _minAmountWorkersForBuildNewBase)
            return;

        if (_flag == null)
            _builder.StartPlacingNewBuilding(_flagPrefab);
        else
            _builder.StartReplacingBuilding(_flag);

        _builder.BuildingPlaced += OnFlagPlaced;
    }

    public void TakeResource(Resource resource)
    {
        _storage.TakeResource(resource);
        resource.Remove();
    }

    public void AddWorker(Worker worker)
    {
        _workers.Add(worker);
        _workersCoordinator.AddWorker(worker);

        worker.Init(this, _builder, _scanner, _mouseInput);
    }

    public void RemoveWorker(Worker worker)
    {
        _workers.Remove(worker);
        _workersCoordinator.RemoveWorker(worker);
    }

    private void OnFlagPlaced(Building flag)
    {
        _builder.BuildingPlaced -= OnFlagPlaced;
        _flag = flag as Flag;

        if (_flag == null)
            return;

        _flag.transform.SetParent(transform);

        SetBaseBuildMode();
    }

    private void BuildNewBase()
    {
        _storage.TakeAwayResources(_resourcesOnNewBase);

        _workersCoordinator.BuildBase(_flag.transform.position);
        _workersCoordinator.BaseBuilded += OnBaseBuilded;
    }

    private void OnBaseBuilded()
    {
        Destroy(_flag.gameObject);
        _flag = null;

        SetMiningMode();
    }

    private void OnResourceReceived()
    {
        _counter.Count();
    }

    private void BuyWorker()
    {
        Worker worker = _workerSpawner.Spawn(GetSpawnPositionWithScatter());
        AddWorker(worker);
        _storage.TakeAwayResources(_buyWorkerPerResource);
    }

    private void StartMiningResources()
    {
        List<Vector3> resources = 
            _scanner.GiveResourcesNearby(transform.position, _resourceExtractionRadius);
        _workersCoordinator.MiningResources(resources);
    }

    private void SetMiningMode()
    {
        ResetModes();

        _counter.CountReached += BuyWorker;
        _counter.SetTargetNumber(_buyWorkerPerResource);
    }

    private void SetBaseBuildMode()
    {
        ResetModes();

        _counter.CountReached += BuildNewBase;
        _counter.SetTargetNumber(_resourcesOnNewBase);
    }

    private void ResetModes()
    {
        _counter.CountReached -= BuyWorker;
        _counter.CountReached -= BuildNewBase;
    }

    private Vector3 GetSpawnPositionWithScatter()
    {
        float height = 1;
        float scatter = Random.value;
        Vector3 offSet = new(scatter, height, scatter);

        return transform.position + offSet;
    }

    private void OnLMBClicked(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Base @base))
            @base.StartSetFlag();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _resourceExtractionRadius);
    }
}