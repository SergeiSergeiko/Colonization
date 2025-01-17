using UnityEngine;

public class PickUper
{
    public Resource PickUpResource(Vector3 position, float radius)
    {
        Collider[] colliders =
            Physics.OverlapSphere(position, radius);

        foreach (Collider collider in colliders)
            if (collider.TryGetComponent(out Resource resource))
                return resource;

        return null;
    }
}