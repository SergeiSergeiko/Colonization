using System;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public event Action<Effect> Stoped;

    private void OnParticleSystemStopped()
    {
        Stoped?.Invoke(this);
    }
}