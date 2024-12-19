using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private int _gold;
    private int _wood;

    public Action<int> GoldChanged;
    public Action<int> WoodChanged;

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

    public bool TryToPay(Price2Int price)
    {
        if (EnoughMoney(price))
        {
            Gold -= price.Gold;
            Wood -= price.Wood;

            return true;
        }

        return false;
    }

    public bool EnoughMoney(Price2Int price)
    {
        return Gold >= price.Gold && Wood >= price.Wood;
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