using TMPro;
using UnityEngine;

public class UIPriceRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _gold;
    [SerializeField] private TMP_Text _wood;
    [SerializeField] private ShopWorkers _shop;

    private void Start()
    {
        SetPrice(_shop.Price);
    }

    private void SetPrice(Price2Int price)
    {
        _gold.text = price.Gold.ToString();
        _wood.text = price.Wood.ToString();
    }
}