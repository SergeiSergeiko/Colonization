using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public class Plane : MonoBehaviour
{
    private Vector3 _planeSize;
    private MeshRenderer _mesh;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _planeSize = GetSize();
    }

    public Vector3 GetRandomPosition()
    {
        float divider = 3f;
        float rangeX = _planeSize.x / divider;
        float rangeZ = _planeSize.z / divider;

        float randomX = Random.Range(-rangeX, rangeX);
        float randomZ = Random.Range(-rangeZ, rangeZ);

        Vector3 position = transform.position
            + new Vector3(randomX, 0, randomZ);

        return position;
    }

    private Vector3 GetSize()
    {
        Vector3 size = Vector3.Scale(_mesh.bounds.size, transform.localScale);

        return size;
    }
}