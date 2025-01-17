public class WorkerSpawner : Spawner<Worker>
{
    public WorkerSpawner(Worker prefab) : base(prefab)
    {
        Prefab = prefab;
    }
}