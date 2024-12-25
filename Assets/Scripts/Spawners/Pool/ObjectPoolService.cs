using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolService : MonoBehaviour 
{
    private readonly Dictionary<GameObject, GameObjectPool> _poolsMap = new(32);
    private readonly Dictionary<GameObject, GameObject> _spawnedGameObjectsMap = new(256);

    [SerializeField] private PoolSettings _defaultSettings;

    public IEnumerable<GameObject> SpawnedGameObjects => _spawnedGameObjectsMap.Keys;

    public int SpawnedGameObjectsCount => _spawnedGameObjectsMap.Count;

    public List<Transform> GetResourceTransforms()
    {
        return SpawnedGameObjects
            .Where(x => x.TryGetComponent(out Resource _))
            .Select(x => x.transform)
            .ToList();
    }

    public T Spawn<T>(T prefab) where T : Component
    {
        GameObject instance = Spawn(prefab.gameObject);
        return instance.GetComponent<T>();
    }

    public GameObject Spawn(GameObject prefab)
    {
        GameObjectPool pool = GetOrCreatePool(prefab);
        GameObject instance = pool.Get();
        _spawnedGameObjectsMap.Add(instance, prefab);
        return instance;
    }

    public void Despawn(Component prefab)
    {
        Despawn(prefab.gameObject);
    }

    public void Despawn(GameObject instance)
    {
        GameObject prefab = _spawnedGameObjectsMap[instance];
        GameObjectPool pool = _poolsMap[prefab];
        pool.Release(instance);
        _spawnedGameObjectsMap.Remove(instance);
    }

    public GameObjectPool GetOrCreatePool(Component prefab)
    {
        return GetOrCreatePool(prefab.gameObject);
    }

    public GameObjectPool GetOrCreatePool(GameObject prefab)
    {
        return _poolsMap.TryGetValue(prefab, out GameObjectPool pool)
            ? pool
            : CreatePool(prefab);
    }

    public GameObjectPool CreatePool(Component prefab, PoolSettings settings = null)
    {
        return CreatePool(prefab.gameObject, settings);
    }

    public GameObjectPool CreatePool(GameObject prefab, PoolSettings settings = null)
    {
#if DEBUG
        if (_poolsMap.ContainsKey(prefab))
            throw new Exception($"The pool with prefab '{prefab.name}' already exists!");
#endif
        var newPool = new GameObjectPool(prefab, settings ?? _defaultSettings);
        _poolsMap.Add(prefab, newPool);
        return newPool;
    }

    public bool HasPool(Component prefab)
    {
        return HasPool(prefab.gameObject);
    }

    public bool HasPool(GameObject prefab)
    {
        return _poolsMap.ContainsKey(prefab);
    }
}