using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Plane _plane;
    [SerializeField] private List<Resource> _prefabs;
    [SerializeField] private ObjectPoolService _objectPoolService;
    [SerializeField] private float _spawnTime;

    private bool _isSpawn = true;

    private void Start()
    {
        StartCoroutine(SpawnGoldPerTime());
    }

    private IEnumerator SpawnGoldPerTime()
    {
        WaitForSeconds wait = new(_spawnTime);

        while (_isSpawn)
        {
            Resource resource = GetRandomResource();
            Resource instance = _objectPoolService.Spawn(resource);
            Subscribe(instance);
            instance.transform.position = _plane.GetRandomPositionOnPlane();

            yield return wait;
        }
    }

    private void Subscribe(Resource resource)
    {
        resource.Removed += Despawn;
    }

    private void Despawn(Resource resource)
    {
        resource.Removed -= Despawn;
        _objectPoolService.Despawn(resource);
    }

    private Resource GetRandomResource()
    {
        int index = Random.Range(0, _prefabs.Count);

        return _prefabs[index];
    }
}