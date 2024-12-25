using System.Collections.Generic;
using UnityEngine;

public class OutlineEffectSpawner : EffectSpawner<OutlineEffect>
{
    [SerializeField] private Scanner _scanner;

    private void Awake()
    {
        _scanner.ResourcesScanned += OnResourcesScanned;
    }

    private void OnDisable()
    {
        _scanner.ResourcesScanned -= OnResourcesScanned;
    }

    private void OnResourcesScanned(List<Transform> resources)
    {
        foreach (Transform resource in resources)
            Spawn(resource.position);
    }
}