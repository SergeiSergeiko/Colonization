public class EffectSpawner<T> : Spawner<Effect> where T : Effect
{
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