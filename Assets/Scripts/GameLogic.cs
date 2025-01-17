using System.Collections;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Builder _builder;

    [Header("Resource")]
    [SerializeField] private Plane _plane;
    [SerializeField] private Resource _resourcePrefab;

    [Header("Base")]
    [SerializeField] private Transform _baseSpawnPoint;
    [SerializeField] private Base _basePrefab;

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
        StartCoroutine(
            _resourceSpawner.SpawnResourcePerTime(_spawningTime), _resourceSpawning);

        Building building = _builder.Build(_basePrefab, _baseSpawnPoint.position);
        if (building is Base @base)
            @base.Init(_builder);
        else
            Debug.LogError("building - is not a Base");
    }

    private void OnDisable()
    {
        StopCoroutine(_resourceSpawning);
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