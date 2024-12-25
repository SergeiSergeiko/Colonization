using System;
using UnityEngine;

[Serializable]
public class PoolSettings
{
    [SerializeField] private bool _collectionCheck = true;
    [SerializeField, Min(0)] private int _capacity = 10;
    [SerializeField, Min(0)] private int _maxSize = 128;

    public bool CollectionCheck => _collectionCheck;

    public int Capacity => _capacity;

    public int MaxSize => _maxSize;
}