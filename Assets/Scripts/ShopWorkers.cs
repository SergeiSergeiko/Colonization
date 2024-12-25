using UnityEngine;

public class ShopWorkers : MonoBehaviour
{
    [SerializeField] private Worker _prefab;
    [SerializeField] private ObjectPoolService _objectPoolService;

    public Worker BuyWorker()
    {
        return _objectPoolService.Spawn(_prefab);
    }
}