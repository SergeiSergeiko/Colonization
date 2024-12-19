using System;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private ObjectPoolService _objectPoolService;

    public Action<List<Transform>> ResourcesScanned;

    public List<Transform> ScanOnGold()
    {
        List<Transform> foundResources = _objectPoolService.GetResourceTransforms();

        ResourcesScanned?.Invoke(foundResources);

        return foundResources;
    }
}