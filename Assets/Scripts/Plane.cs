using UnityEngine;

public class Plane : MonoBehaviour
{
    private Vector3 _planeSize;

    private void Awake()
    {
        _planeSize = GetPlaneSize();
    }

    public Vector3 GetRandomPositionOnPlane()
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

    private Vector3 GetPlaneSize()
    {
        Vector3 planeSize = Vector3.Scale(GetComponent<MeshRenderer>().bounds.size,
            transform.localScale);

        return planeSize;
    }
}