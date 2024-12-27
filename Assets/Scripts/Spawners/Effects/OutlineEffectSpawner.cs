using System.Collections.Generic;
using UnityEngine;

public class OutlineEffectSpawner : EffectSpawner<OutlineEffect>
{
    public void Spawn(List<Transform> resources)
    {
        foreach (Transform resource in resources)
            Spawn(resource.position);
    }
}