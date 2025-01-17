using System.Collections;
using UnityEngine;

public class ResourceSpawner : Spawner<Resource>
{
    private Plane _plane;
    private bool _isWorking = true;

    public ResourceSpawner(Resource prefab, Plane plane) : base(prefab)
    {
        Prefab = prefab;
        _plane = plane;
    }

    public IEnumerator SpawnResourcePerTime(float spawnTime)
    {
        WaitForSeconds wait = new(spawnTime);

        while (_isWorking)
        {
            yield return wait;

            Spawn(_plane.GetRandomPosition());
        }
    }

    protected override void Subscribe(Resource resource)
    {
        resource.Removed += Despawn;
    }

    protected override void Despawn(Resource resource)
    {
        resource.Removed -= Despawn;
        Realese(resource);
    }
}