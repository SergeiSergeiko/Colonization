using System.Collections;
using UnityEngine;

public class ResourceSpawner<T> : Spawner<Resource>
{
    [SerializeField] private Plane _plane;
    [SerializeField] private float _spawnTime;

    private void Start()
    {
        StartCoroutine(SpawnResourcePerTime());
    }

    private IEnumerator SpawnResourcePerTime()
    {
        WaitForSeconds wait = new(_spawnTime);

        while (enabled)
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