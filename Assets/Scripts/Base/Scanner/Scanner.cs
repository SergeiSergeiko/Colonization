using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Vector3 _size;
    [SerializeField, Min(1f)] private float _delay;
    [SerializeField] private ObjectPoolService _objectPoolService;
    [SerializeField] private ScannerEffectSpawner _scannerEffectSpawner;

    private Coroutine _scanning;

    public event Action<List<Transform>> ResourcesScanned;

    public void EnableScan()
    {
        if (_scanning != null)
            StopCoroutine(_scanning);

        _scanning = StartCoroutine(Scanning());
    }

    private IEnumerator Scanning()
    {
        WaitForSeconds wait = new(_delay);

        yield return wait;

        while (enabled)
        {
            ScanOnResources();

            yield return wait;
        }
    }

    private void ScanOnResources()
    {
        List<Transform> foundResources = new();

        Collider[] colliders = Physics.OverlapBox(transform.position, _size);

        foreach (Collider collider in colliders)
            if (collider.TryGetComponent(out Resource _))
                foundResources.Add(collider.transform);

        ResourcesScanned?.Invoke(foundResources);
        _scannerEffectSpawner.Spawn(transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}