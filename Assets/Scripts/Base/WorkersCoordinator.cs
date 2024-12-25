using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersCoordinator : MonoBehaviour
{
    [SerializeField] private Base _base;

    private List<Transform> _foundResources = new();
    private Coroutine _working;

    public void StartWorking(List<Transform> resources)
    {
        _foundResources = resources;

        if (_working != null)
            StopCoroutine(_working);

        _working = StartCoroutine(Working());
    }

    private IEnumerator Working()
    {
        WaitUntil waitFreeWorker = new(() => _base.CountFreeWorkers > 0);

        while (_foundResources.Count > 0)
        {
            if (_base.TryGetFreeWorker(out Worker worker))
            {
                Transform resource = _foundResources[0];
                worker.ExtractResource(resource);
                _foundResources.Remove(resource);

                yield return null;
            }
            else
            {
                yield return waitFreeWorker;
            }
        }
    }
}