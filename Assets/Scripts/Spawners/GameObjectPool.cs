using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public class GameObjectPool<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly ObjectPool<T> _pool;
    private readonly Transform _parent;

    public GameObjectPool(T prefab)
    {
        _prefab = prefab;
        _pool = new ObjectPool<T>(
            Create, OnGet, OnRelease, OnDestroy, true, 10, 64);
        _parent = new GameObject($"GameObject Pool <{_prefab.name}>").transform;
    }

    public T Get()
    {
        return _pool.Get();
    }

    public void Release(T obj)
    {
        _pool.Release(obj);
    }

    private T Create()
    {
        return Object.Instantiate(_prefab);
    }

    private void OnGet(T obj)
    {
        obj.transform.SetParent(null);
        obj.gameObject.SetActive(true);
    }

    private void OnRelease(T obj)
    {
        obj.transform.SetParent(_parent);
        obj.gameObject.SetActive(false);
    }

    private void OnDestroy(T obj)
    {
        Object.Destroy(obj.gameObject);
    }
}