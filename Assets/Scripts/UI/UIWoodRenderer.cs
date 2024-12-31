public class UIWoodRenderer : UIResourceRenderer
{
    private void Awake()
    {
        Storage.WoodChanged += RefreshText;
    }

    private void OnDisable()
    {
        Storage.WoodChanged -= RefreshText;
    }

    protected override void RefreshText()
    {
        Text.text = Storage.Wood.ToString();
    }
}