using System;
using UniRx;

public class FoodSpawnRunner : IEntityRule
{
    public EntityId Id => EntityId.Food;

    private readonly IEntitySpawner spawner;
	private readonly ICubeArea spawnZone;
	private readonly CompositeDisposable disp = new();

	public FoodSpawnRunner(
		IEntitySpawner spawner,
		ICubeArea spawnZone)
	{
		this.spawner = spawner;
		this.spawnZone = spawnZone;
	}

	public void Initialize()
	{
		Observable.Range(0, 50)
			.Select(_ => Unit.Default)
			.Concat(
			Observable.Interval(TimeSpan.FromSeconds(1))
			.Select(_ => Unit.Default))
			.Subscribe(_ =>
			spawner.SpawnEntityRandomRotation(EntityId.Food, spawnZone.GetRandomInsideZone()))
			.AddTo(disp);
	}
	public void SubscribeOnEntity(ISplitable splitable)
	{
		
	}

	public void Dispose()
	{
		disp.Dispose();
	}
}