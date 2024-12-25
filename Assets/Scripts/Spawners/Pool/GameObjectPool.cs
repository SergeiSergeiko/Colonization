using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public class GameObjectPool : IDisposable
{
    private readonly GameObject _prefab;
    private readonly PoolSettings _settings;
    private readonly ObjectPool<GameObject> _pool;
    private readonly Transform _parent;

    public GameObjectPool(GameObject prefab, PoolSettings settings)
    {
        _prefab = prefab;
        _settings = settings;
        _pool = new ObjectPool<GameObject>(
            Create, OnGet, OnRelease, OnDestroy,
            _settings.CollectionCheck, _settings.Capacity, _settings.MaxSize);
        _parent = new GameObject($"GameObject Pool <{_prefab.name}>").transform;
    }

    public int MaxSize => _settings.MaxSize;

    public int CountAll => _pool.CountAll;

    public int CountActive => _pool.CountActive;

    public int CountInactive => _pool.CountInactive;

    public GameObject Get()
    {
        return _pool.Get();
    }

    public void Release(GameObject instance)
    {
        _pool.Release(instance);
    }

    public void Clear()
    {
        _pool.Clear();
    }

    public void Dispose()
    {
        _pool.Dispose();
        Object.Destroy(_parent.gameObject);
    }

    private GameObject Create()
    {
        return Object.Instantiate(_prefab);
    }

    private void OnGet(GameObject instance)
    {
        instance.transform.SetParent(null);
        instance.gameObject.SetActive(true);
    }

    private void OnRelease(GameObject instance)
    {
        instance.transform.SetParent(_parent);
        instance.gameObject.SetActive(false);
    }

    private void OnDestroy(GameObject instance)
    {
        Object.Destroy(instance);
    }
}