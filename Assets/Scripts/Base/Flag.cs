using UnityEngine;

public class Flag : Building
{
    [SerializeField] private Renderer _renderer;

    private void Start()
    {
        SetRandomColor();
    }

    private void SetRandomColor()
    {
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}