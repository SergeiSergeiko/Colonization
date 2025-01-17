using UnityEngine;

public class Spawner<T> where T : MonoBehaviour
{
    protected T Prefab;
    protected GameObjectPool<T> Pool;

    public Spawner(T prefab)
    {
        Prefab = prefab;
        Pool = new GameObjectPool<T>(Prefab);
    }

    public T Spawn(Vector3 position)
    {
        T obj = Pool.Get();
        Subscribe(obj);
        obj.transform.position = position;

        return obj;
    }

    protected virtual void Subscribe(T obj) { }

    protected virtual void Despawn(T obj) { }

    protected void Realese(T obj)
    {
        Pool.Release(obj);
    }
}