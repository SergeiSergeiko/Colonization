using System.Collections;
using UnityEngine;

public class ResourceSpawner<T> : Spawner<Resource>
{
    [SerializeField] private Plane _plane;
    [SerializeField] private float _spawnTime;

    private bool _isSpawn = true;

    private void Start()
    {
        StartCoroutine(SpawnResourcePerTime());
    }

    private IEnumerator SpawnResourcePerTime()
    {
        WaitForSeconds wait = new(_spawnTime);

        yield return wait;

        while (_isSpawn)
        {
            Spawn(_plane.GetRandomPosition());

            yield return wait;
        }
    }

    public override void Spawn(Vector3 position)
    {
        Resource resource = ObjectPoolService.Spawn(Prefab);
        Subscribe(resource);
        resource.transform.position = position;
    }

    protected override void Subscribe(Resource resource)
    {
        resource.Removed += Despawn;
    }

    protected override void Despawn(Resource resource)
    {
        resource.Removed -= Despawn;
        ObjectPoolService.Despawn(resource);
    }
}