using UnityEngine;

public class ConstructionCoordinator
{
    public void Build(Worker worker, Vector3 position)
    {
        worker.BuildBase(position);
    }
}