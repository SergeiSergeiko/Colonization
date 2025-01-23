using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkersCoordinator : MonoBehaviour
{
    private List<Worker> _workers = new();
    private ConstructionCoordinator _constructionCoordinator = new();
    private MiningCoordinator _miningCoordinator = new();

    private Coroutine _buildingBase;
    private Coroutine _miningWorking;

    private Worker _freeWorker;

    public event Action BaseBuilded;

    public void AddWorker(Worker worker) => _workers.Add(worker);

    public void RemoveWorker(Worker worker) => _workers.Remove(worker);

    public void MiningResources(List<Vector3> resources)
    {
        if (_miningWorking != null)
        {
            _miningCoordinator.EndWork();
            StopCoroutine(_miningWorking);
        }

        _miningWorking = StartCoroutine(_miningCoordinator.Working(resources, _workers));
    }

    public void BuildBase(Vector3 position)
    {
        if (_buildingBase != null)
            StopCoroutine(_buildingBase);

        _buildingBase = StartCoroutine(ProcessSendingBuildBase(position));
    }

    private IEnumerator ProcessSendingBuildBase(Vector3 position)
    {
        _freeWorker = GetFreeWorker();

        if (_freeWorker == null)
            yield return StartCoroutine(WaitingFreedWorker());

        _constructionCoordinator.Build(_freeWorker, position);
        _freeWorker.Freed += OnBaseBuilded;

        _buildingBase = null;
    }

    private void OnBaseBuilded(Worker worker)
    {
        worker.Freed -= OnBaseBuilded;
        BaseBuilded?.Invoke();
    }

    private Worker GetFreeWorker()
    {
        return _workers.FirstOrDefault(worker => worker.IsBusy == false);
    }

    private void OnFreedWorker(Worker worker)
    {
        _workers.ForEach(worker => worker.Freed -= OnFreedWorker);

        _freeWorker = worker;
    }

    private IEnumerator WaitingFreedWorker()
    {
        WaitUntil wait = new(() => _freeWorker != null);
        bool isWorking = true;

        _workers.ForEach(worker => worker.Freed += OnFreedWorker);

        while (isWorking)
        {
            yield return wait;
            isWorking = false;
        }
    }
}