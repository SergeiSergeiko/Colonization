using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public event Action<List<Transform>> ResourcesScanned;

    private void Awake()
    {
        SetSizeScannerEffect(_scannerEffectSize);

        _scannerEffectSpawner = new(_scannerEffectPrefab);
        _outlineEffectSpawner = new(_outlineEffectPrefab);
    }

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

    private void SetSizeScannerEffect(int factor)
    {
        ParticleSystem.MainModule main
            = _scannerEffectPrefab.GetComponent<ParticleSystem>().main;
        main.startSize = _size.x * factor;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}