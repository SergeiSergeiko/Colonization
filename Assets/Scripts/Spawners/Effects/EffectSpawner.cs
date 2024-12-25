using UnityEngine;

public class EffectSpawner<T> : Spawner<Effect> where T : Effect
{
    public override void Spawn(Vector3 position)
    {
        Effect effect = ObjectPoolService.Spawn(Prefab);
        Subscribe(effect);
        effect.transform.position = position;
    }

    protected override void Subscribe(Effect effect)
    {
        effect.Stoped += Despawn;
    }

    protected override void Despawn(Effect effect)
    {
        effect.Stoped -= Despawn;
        ObjectPoolService.Despawn((T)effect);
    }
}