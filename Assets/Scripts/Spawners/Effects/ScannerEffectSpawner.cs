public class ScannerEffectSpawner : EffectSpawner<ScannerEffect>
{
    public ScannerEffectSpawner(Effect prefab) : base(prefab)
    {
        Prefab = prefab;
    }
}