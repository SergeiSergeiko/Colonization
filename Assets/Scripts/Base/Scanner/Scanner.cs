using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Vector3 _size;
    [SerializeField, Min(1f)] private float _delay;
    [SerializeField] private ScannerEffectSpawner _scannerEffectSpawner;
    [SerializeField] private OutlineEffectSpawner _outlineEffectSpawner;

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

        while (enabled)
        {
            yield return wait;

            ScanOnResources();
        }
    }

    private void ScanOnResources()
    {
        List<Transform> foundResources = new();

        Collider[] colliders = Physics.OverlapBox(transform.position, _size);

        foreach (Collider collider in colliders)
            if (collider.TryGetComponent(out Resource _))
                foundResources.Add(collider.transform);


        _outlineEffectSpawner.Spawn(foundResources);
        _scannerEffectSpawner.Spawn(transform.position);
        ResourcesScanned?.Invoke(foundResources);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}