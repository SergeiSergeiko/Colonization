using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private int _gold;
    private int _wood;

    public event Action<int> GoldChanged;
    public event Action<int> WoodChanged;

    public int Gold 
    {
        get => _gold;

        private set
        {
            _gold = value;
            GoldChanged?.Invoke(_gold);
        }
    }

    public int Wood 
    {
        get => _wood;

        private set
        {
            _wood = value;
            WoodChanged?.Invoke(_wood);
        }
    }

    private void Start()
    {
        Gold = 10;
        Wood = 10;
    }

    public void TakeResource(Resource resource)
    {
        switch (resource)
        {
            case Gold gold:
                Gold += gold.Value;
                break;

            case Wood wood:
                Wood += wood.Value;
                break;

            default:
                Debug.LogError("Resource is not one of the types");
                break;
        }
    }
}