using System;
using UnityEngine;

public class CircleEffect : MonoBehaviour
{
    public Action<CircleEffect> Stoped;

    private void OnParticleSystemStopped()
    {
        Stoped?.Invoke(this);
    }
}