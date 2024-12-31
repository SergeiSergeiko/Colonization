using TMPro;
using UnityEngine;

public class UIResourceRenderer : MonoBehaviour
{
    [SerializeField] protected TMP_Text Text;
    [SerializeField] protected Storage Storage;

    protected virtual void RefreshText() { }
}