using UnityEngine;

public class ShopWorkers : MonoBehaviour
{
    [SerializeField] private Worker _prefab;
    [SerializeField] private ObjectPoolService _objectPoolService;

    [Header("Price")]
    [SerializeField] private int _gold;
    [SerializeField] private int _wood;

    private Price2Int _price;

    public Price2Int Price { get; private set; }

    private void Awake()
    {
        Price = new Price2Int(_gold, _wood);
    }

    public Worker BuyWorker()
    {
        return _objectPoolService.Spawn(_prefab);
    }
}