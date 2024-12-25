﻿using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public event Action<Resource> Removed;

    [field: SerializeField] public int Value { get; private set; }

    public void Remove()
    {
        Removed?.Invoke(this);
    }
}