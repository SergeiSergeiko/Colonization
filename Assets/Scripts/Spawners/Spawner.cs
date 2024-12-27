using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;

    protected GameObjectPool<T> Pool;

    private void Awake()
    {
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