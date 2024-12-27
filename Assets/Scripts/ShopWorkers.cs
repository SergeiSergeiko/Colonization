using UnityEngine;

public class ShopWorkers : MonoBehaviour
{
    [SerializeField] private Worker _prefab;
    [SerializeField] private WorkerSpawner _workerSpawner;

    public Worker BuyWorker(Vector3 position)
    {
        return _workerSpawner.Spawn(position);
    }
}