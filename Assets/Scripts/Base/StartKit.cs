using UnityEngine;

public class StartKit : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private ShopWorkers _shopWorkers;
    [SerializeField] private int _countStartWorkers;

    private void Start()
    {
        for (int i = 0; i < _countStartWorkers; i++)
            _base.AddWorker(_shopWorkers.BuyWorker());
    }
}