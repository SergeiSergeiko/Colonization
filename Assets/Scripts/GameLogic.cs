using System.Collections;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Builder _builder;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private MouseInput _mouseInput;

    [Header("Resource")]
    [SerializeField] private Plane _plane;
    [SerializeField] private Resource _resourcePrefab;

    [Header("Base")]
    [SerializeField] private Transform _baseSpawnPoint;
    [SerializeField] private Base _basePrefab;
    [SerializeField] private int _startAmountWorkers;

    [Header("Resources Spawning Time")]
    [SerializeField] private float _spawningTime;

    private ResourceSpawner _resourceSpawner;
    private Coroutine _resourceSpawning;

    private void Awake()
    {
        _resourceSpawner = new(_resourcePrefab, _plane);
    }

    private void Start()
    {
        ActivateScan();
        StartResourceSpawner();
        InstallBase();
    }

    private void OnDisable()
    {
        StopCoroutine(_resourceSpawning);
    }
    
    private void ActivateScan()
    {
        _scanner.EnableScan();
    }

    private void StartResourceSpawner()
    {
        StartCoroutine(
            _resourceSpawner.SpawnResourcePerTime(_spawningTime), _resourceSpawning);
    }

    private void InstallBase()
    {
        Building building = _builder.Build(_basePrefab, _baseSpawnPoint.position);

        if (building is Base @base)
            @base.Init(_builder, _scanner, _mouseInput, _startAmountWorkers);
        else
            Debug.LogError("building - is not a Base");
    }

    private void StartCoroutine(IEnumerator routine, Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        StartCoroutine(routine);
    }

    private new void StopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
            base.StopCoroutine(coroutine);
    }
}