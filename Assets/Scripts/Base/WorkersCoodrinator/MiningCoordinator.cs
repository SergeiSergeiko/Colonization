using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MiningCoordinator
{
    private List<Worker> _freeWorkers = new();
    private List<Worker> _busyWorkers = new();
    private List<Transform> _foundResources = new();

    public void EndWork()
    {
        _busyWorkers.ForEach(worker => worker.Freed -= OnWorkerFreed);
    }

    public IEnumerator Working(List<Transform> resources, List<Worker> workers)
    {
        WaitUntil waitFreeWorker = new(() => _freeWorkers.Count > 0);

        Init(resources, workers);

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

        EndWork();
    }

    private void Init(List<Transform> resources, List<Worker> workers)
    {
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

    private void SendWorker(Worker worker, Transform resource)
    {
        worker.ExtractResource(resource.position);
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
}