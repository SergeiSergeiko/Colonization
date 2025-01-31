using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Effect : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    public event Action<Effect> Stoped;

    private void OnParticleSystemStopped()
    {
        Stoped?.Invoke(this);
    }

    public void SetSizeScannerEffect(float size)
    {
        Init();

        ParticleSystem.MainModule main = _particleSystem.main;
        main.startSize = size;
    }

    private void Init()
    {
        if (_particleSystem == null)
            _particleSystem = GetComponent<ParticleSystem>();
    }
}