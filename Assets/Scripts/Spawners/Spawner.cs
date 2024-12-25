using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected ObjectPoolService ObjectPoolService;

    public abstract void Spawn(Vector3 position);

    protected abstract void Subscribe(T instance);

    protected abstract void Despawn(T instance);
}