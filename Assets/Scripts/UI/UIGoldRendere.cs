public class UIGoldRenderer : UIResourceRenderer
{
    private void Awake()
    {
        Storage.GoldChanged += RefreshText;
    }

    private void OnDisable()
    {
        Storage.GoldChanged -= RefreshText;
    }

    protected override void RefreshText()
    {
        Text.text = Storage.Gold.ToString();
    }
}