using UnityEngine;

public class StartKit : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private ShopWorkers _shopWorkers;
    [SerializeField] private int _countWorkers;

    private void Start()
    {
        for (int i = 0; i < _countWorkers; i++)
            _base.AddWorker(_shopWorkers.BuyWorker(_base.SpawnPoint));
    }
}