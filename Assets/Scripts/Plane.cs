using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public class Plane : MonoBehaviour
{
    private Vector3 _size;
    private MeshRenderer _mesh;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _size = GetSize();
    }

    public Vector3 GetRandomPosition()
    {
        float divider = 3f;
        float rangeX = _size.x / divider;
        float rangeZ = _size.z / divider;

        float randomX = Random.Range(-rangeX, rangeX);
        float randomZ = Random.Range(-rangeZ, rangeZ);

        return transform.position + new Vector3(randomX, 0, randomZ);
    }

    private Vector3 GetSize()
    {
        return Vector3.Scale(_mesh.bounds.size, transform.localScale);
    }
}