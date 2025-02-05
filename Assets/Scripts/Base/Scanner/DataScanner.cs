using System.Collections.Generic;

public class DataScanner
{
    private List<Resource> _foundResources = new();

    public int CountResources => _foundResources.Count;

    public Resource GetResource()
    {
        Resource resource = _foundResources[0];
        RemoveAtResource(0);
        
        return resource;
    }

    public void AddResource(Resource resource)
    {
        _foundResources.Add(resource);
    }

    private void RemoveAtResource(int index)
    {
        _foundResources.RemoveAt(index);
    }
}