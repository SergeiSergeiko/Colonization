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
}