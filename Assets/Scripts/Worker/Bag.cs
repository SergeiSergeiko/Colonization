using UnityEngine;

public class Bag : MonoBehaviour
{
    public Resource Resource { get; private set; }

    public bool TryGiveResource(out Resource resource)
    {
        if (Resource != null)
        {
            resource = Resource;
            Resource = null;

            return true;
        }

        resource = null;
        return false;
    }

    public void TakeResource(Resource resource)
    {
        Resource = resource;
        resource.transform.SetParent(transform);
    }
}