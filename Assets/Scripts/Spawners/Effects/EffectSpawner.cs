public class EffectSpawner<T> : Spawner<Effect> where T : Effect
{
    public EffectSpawner(Effect prefab) : base(prefab)
    {
        Prefab = prefab;
    }

    protected override void Subscribe(Effect effect)
    {
        effect.Stoped += Despawn;
    }

    protected override void Despawn(Effect effect)
    {
        effect.Stoped -= Despawn;
        Realese((T)effect);
    }
}