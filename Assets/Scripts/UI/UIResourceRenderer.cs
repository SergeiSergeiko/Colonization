using TMPro;
using UnityEngine;

public class UIResourceRenderer : MonoBehaviour
{
    [SerializeField] protected TMP_Text Text;
    [SerializeField] protected Storage Storage;

    private void OnEnable()
    {
        Storage.QuantityChanged += RefreshText;
    }

    private void OnDisable()
    {
        Storage.QuantityChanged -= RefreshText;
    }

    protected virtual void RefreshText()
    {
        Text.text = Storage.Resources.ToString();
    }
}