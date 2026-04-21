using UniRx;
using UnityEngine;

public class WorkerSpawnRunner : IEntityRule
{
    public EntityId Id => EntityId.AntWorker;

    private readonly IEntitySpawner spawner; 
	private readonly IEntityTracker tracker;
	private readonly ICubeArea spawnZone;
	private readonly CompositeDisposable disp = new();
	private readonly int foodEatenForSplit = 2; // from config

    public WorkerSpawnRunner(
		IEntitySpawner spawner,
		IEntityTracker tracker,
		ICubeArea spawnZone)
	{
		this.spawner = spawner;
		this.tracker = tracker;
		this.spawnZone = spawnZone;
	}

	public void Initialize()
	{
		spawner.SpawnEntityRandomRotation(EntityId.AntWorker, spawnZone.GetRandomInsideZone());

		tracker.ActiveEntities
			.ObserveValue(EntityId.AntWorker)
			.Where(x => x == 0)
			.Subscribe(_ => 
			spawner.SpawnEntityRandomRotation(EntityId.AntWorker, spawnZone.GetRandomInsideZone()))
			.AddTo(disp);
	}
	public void SubscribeOnEntity(ISplitable splitable)
	{
		splitable.Root.TryGetCapability(out IFeedable feedable);

		feedable.EatenCount.
			Where(x => x == foodEatenForSplit).
			Subscribe(_ => splitable.Split(GetEntityIdForSplit(), 1, spawnZone.GetRandomInsideZone()))
			.AddTo(disp);
	}

	public void Dispose()
	{
		disp.Dispose();
	}
	private Vector3 GetEntityCurrentPosition(IEntityComponentRoot root) => 
		root.GameObject.transform.position;
	private EntityId GetEntityIdForSplit()
	{
		EntityId spawnedId = EntityId.AntWorker;
		if (tracker.ActiveEntities[EntityId.AntWorker] > 10)
		{
			int chance = UnityEngine.Random.Range(1, 11);
			if (chance <= 1) spawnedId = EntityId.BugPredator;
		}
		return spawnedId;
	}
}