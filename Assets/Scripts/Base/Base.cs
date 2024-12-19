using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private Button _scanButton;
    [SerializeField] private Button _buyWorkerButton;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Storage _storage;
    [SerializeField] private ShopWorkers _shopWorkers;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _countStartWorkers;

    private List<Worker> _workers = new();

    private void OnEnable()
    {
        _scanButton.onClick.AddListener(ScanOnGold);
        _buyWorkerButton.onClick.AddListener(BuyWorker);
    }

    private void OnDisable()
    {
        _scanButton.onClick.RemoveListener(ScanOnGold);
        _buyWorkerButton.onClick.RemoveListener(BuyWorker);
    }

    private void Start()
    {
        for (int i = 0; i < _countStartWorkers; i++)
            GetWorker(_shopWorkers.BuyWorker());
    }

    public void TakeResource(Resource resource)
    {
        _storage.TakeResource(resource);
        resource.Remove();
    }

    public void GetWorker(Worker worker)
    {
        _workers.Add(worker);
        worker.Init(this);
        worker.transform.position = GetSpawnPositionWithScatter();
    }

    private void BuyWorker()
    {
        if (_storage.TryToPay(_shopWorkers.Price))
        {
            Worker worker = _shopWorkers.BuyWorker();
            GetWorker(worker);
        }
    }

    private Vector3 GetSpawnPositionWithScatter()
    {
        int divider = 10;
        float scatter = Random.value / divider;
        Vector3 offSet = new(scatter, 0, scatter);

        return _spawnPoint.position + offSet;
    }

    private void ScanOnGold()
    {
        var goldPositions = _scanner.ScanOnGold();
        SendWorking(goldPositions);
    }

    private void SendWorking(List<Transform> resources)
    {
        List<Worker> workers = _workers
            .Where(worker => worker.IsBusy == false)
            .ToList();

        int count = Mathf.Min(resources.Count, workers.Count);

        for (int i = 0; i < count; i++)
            workers[i].GoToGetResource(resources[i]);
    }
}