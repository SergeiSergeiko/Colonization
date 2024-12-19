using System.Collections.Generic;
using UnityEngine;

public class CircleEffectSpawner : MonoBehaviour
{
    [SerializeField] private CircleEffect _prefab;
    [SerializeField] private ObjectPoolService _objectPoolService;
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

    private void Spawn(Vector3 position)
    {
        CircleEffect effect = _objectPoolService.Spawn(_prefab);
        Subscribe(effect);
        effect.transform.position = position;
    }

    private void Subscribe(CircleEffect effect)
    {
        effect.Stoped += Despawn;
    }

    private void Despawn(CircleEffect effect)
    {
        effect.Stoped -= Despawn;
        _objectPoolService.Despawn(effect);
    }
}