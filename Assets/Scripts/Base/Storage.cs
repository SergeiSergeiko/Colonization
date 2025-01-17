using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private int _resources;

    public event Action ResourceReceived;
    public event Action QuantityChanged;

    public int Resources 
    {
        get => _resources;

        private set
        {
            _resources = Math.Clamp(value, 0, int.MaxValue);
            QuantityChanged?.Invoke();
        }
    }

    private void Start()
    {
        Resources = 0;
    }

    public void TakeResource(Resource resource)
    {
        if (resource.Value <= 0)
            Debug.LogError($"{resource.name} value is less than or equal to 0");

        Resources += resource.Value;
        ResourceReceived?.Invoke();
    }

    public void TakeAwayResources(int amount)
    {
        Resources -= amount;
    }
}