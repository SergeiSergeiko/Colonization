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
    private List<Worker> _freeWorkers = new();

    public int CountFreeWorkers => _freeWorkers.Count;

    private void OnEnable()
    {
        _scanner.ResourcesScanned += _workersCoordinator.StartWorking;
    }

    private void OnDisable()
    {
        _scanner.ResourcesScanned -= _workersCoordinator.StartWorking;
    }

    private void Start()
    {
        _scanner.EnableScan();
    }

    public void TakeResource(Worker worker, Resource resource)
    {
        _freeWorkers.Add(worker);
        _storage.TakeResource(resource);
        resource.Remove();
    }

    public void AddWorker(Worker worker)
    {
        _workers.Add(worker);
        _freeWorkers.Add(worker);
        worker.Init(this);
        worker.transform.position = GetSpawnPositionWithScatter();
    }

    public bool TryGetFreeWorker(out Worker worker)
    {
        worker = _freeWorkers.FirstOrDefault();

        if (worker != null)
        {
            _freeWorkers.Remove(worker);

            return true;
        }

        return false;
    }

    private Vector3 GetSpawnPositionWithScatter()
    {
        float scatter = Random.value;
        Vector3 offSet = new(scatter, 0, scatter);

        return _spawnPoint.position + offSet;
    }
}