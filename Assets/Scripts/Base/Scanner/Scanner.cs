using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Vector3 _size;
    [SerializeField, Min(1f)] private float _delay;
    [SerializeField] private Effect _scannerEffectPrefab;
    [SerializeField] private Effect _outlineEffectPrefab;
    [SerializeField] private int _scannerEffectSize = 4;

    private ScannerEffectSpawner _scannerEffectSpawner;
    private OutlineEffectSpawner _outlineEffectSpawner;
    private Coroutine _scanning;
    private List<Transform> _foundResources = new();

    public event Action ResourcesScanned;

    private void Awake()
    {
        SetSizeScannerEffect();

        _scannerEffectSpawner = new(_scannerEffectPrefab);
        _outlineEffectSpawner = new(_outlineEffectPrefab);
    }

    public void EnableScan()
    {
        if (_scanning != null)
            StopCoroutine(_scanning);

        _scanning = StartCoroutine(Scanning());
    }

    public List<Vector3> GiveResourcesNearby(Vector3 position, float radius)
    {
        List<Vector3> foundresourcesNearby = _foundResources
        .Where(resource => 
        (position - resource.position).sqrMagnitude <= radius * radius)
        .Select(resource => resource.position)
        .ToList();

        _foundResources
            .RemoveAll(resource => foundresourcesNearby.Contains(resource.position));

        return foundresourcesNearby;
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
        _foundResources.Clear();

        Collider[] colliders = Physics.OverlapBox(transform.position, _size);
        
        foreach (Collider collider in colliders)
            if (collider.TryGetComponent(out Resource _))
                _foundResources.Add(collider.transform);

        _outlineEffectSpawner.Spawn(_foundResources);
        _scannerEffectSpawner.Spawn(transform.position);
        ResourcesScanned?.Invoke();
    }

    private void SetSizeScannerEffect()
    {
        _scannerEffectPrefab.SetSizeScannerEffect(_size.x * _scannerEffectSize);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}