using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private WorkersCoordinator _workersCoordinator;
    [SerializeField] private Storage _storage;
    [SerializeField] private Transform _spawnPoint;

    private List<Worker> _workers = new();

    private void Awake()
    {
        _scanner.ResourcesScanned += StartMiningResources;
    }

    private void OnDisable()
    {
        _scanner.ResourcesScanned -= StartMiningResources;
    }

    private void Start()
    {
        _scanner.EnableScan();
    }

    public void TakeResource(Worker worker, Resource resource)
    {
        _storage.TakeResource(resource);
        resource.Remove();
    }

    public void AddWorker(Worker worker)
    {
        _workers.Add(worker);
        worker.Init(this);
        worker.transform.position = GetSpawnPositionWithScatter();
    }

    private void StartMiningResources(List<Transform> resources)
    {
        _workersCoordinator.StartWorking(resources, _workers);
    }

    private Vector3 GetSpawnPositionWithScatter()
    {
        float scatter = Random.value;
        Vector3 offSet = new(scatter, 0, scatter);

        return _spawnPoint.position + offSet;
    }
}