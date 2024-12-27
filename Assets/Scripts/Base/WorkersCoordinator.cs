using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersCoordinator : MonoBehaviour
{
    [SerializeField] private Base _base;

    private List<Worker> _workers = new();
    private List<Worker> _freeWorkers = new();
    private List<Worker> _busyWorkers = new();
    private List<Transform> _foundResources = new();
    private Coroutine _working;

    private void OnDisable()
    {
        _busyWorkers.ForEach(worker => worker.Freed -= OnWorkerFreed);

        if (_working != null)
            StopCoroutine(_working);
    }

    public void StartWorking(List<Transform> resources, List<Worker> workers)
    {
        InitWorkersCoordinator(resources, workers);

        if (_working != null)
            StopCoroutine(_working);

        _working = StartCoroutine(Working());
    }

    private IEnumerator Working()
    {
        WaitUntil waitFreeWorker = new(() => _freeWorkers.Count > 0);

        while (_foundResources.Count > 0)
        {
            if (_freeWorkers.Count > 0)
            {
                SendWorker(_freeWorkers[0], _foundResources[0]);

                yield return null;
            }
            else
            {
                yield return waitFreeWorker;
            }
        }
    }

    private void SendWorker(Worker worker, Transform resource)
    {
        worker.ExtractResource(resource);
        worker.Freed += OnWorkerFreed;

        _busyWorkers.Add(worker);
        _freeWorkers.Remove(worker);
        _foundResources.Remove(resource);
    }

    private void OnWorkerFreed(Worker worker)
    {
        worker.Freed -= OnWorkerFreed;

        _freeWorkers.Add(worker);
        _busyWorkers.Remove(worker);
    }

    private void InitWorkersCoordinator(List<Transform> resources, List<Worker> workers)
    {
        _workers = workers;
        _foundResources = resources;
        _freeWorkers.Clear();
        _busyWorkers.Clear();

        foreach (Worker worker in workers)
        {
            if (worker.IsBusy)
            {
                worker.Freed += OnWorkerFreed;

                _busyWorkers.Add(worker);
            }
            else
            {
                _freeWorkers.Add(worker);
            }
        }
    }
}