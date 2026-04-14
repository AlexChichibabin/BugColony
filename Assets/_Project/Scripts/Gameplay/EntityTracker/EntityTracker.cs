using System;
using UniRx;

public class EntityTracker : IEntityTracker, IDisposable
{
	public IReadOnlyReactiveDictionary<EntityId, int> ActiveEntities => activeEntities;
	public IReadOnlyReactiveDictionary<EntityId, int> DestructedEntities => destructedEntities;
	public IReactiveCollection<IDestructible> AllActiveEntities => allActiveEntities;

	private ReactiveDictionary<EntityId, int> activeEntities = new();
	private ReactiveDictionary<EntityId, int> destructedEntities = new();

	private ReactiveCollection<IDestructible> allActiveEntities = new();

	private IConfigProvider configProvider;
	private IEntityPool pool;

	private CompositeDisposable disp = new();

	public EntityTracker(
		IConfigProvider configProvider,
		IEntityPool pool)
	{
		this.configProvider = configProvider;
		this.pool = pool;
	}

	private void AddActiveEntity(EntityId entityId, IDestructible dest, int count)
	{
		if (count <= 0) return;
		if (activeEntities.ContainsKey(entityId))
			activeEntities[entityId] += count;
		else
			activeEntities.Add(entityId, count);

		allActiveEntities.Add(dest);
	}
	private void RemoveActiveEntity(EntityId entityId, IDestructible dest, int count)
	{
		if (count <= 0) return;
		if (activeEntities.ContainsKey(entityId))
			activeEntities[entityId] = Math.Max(0, activeEntities[entityId] - count);

		allActiveEntities.Remove(dest);

		AddDestructedEntity(entityId, count);
	}
	private void AddDestructedEntity(EntityId entityId, int count)
	{
		if (count <= 0) return;
		if (destructedEntities.ContainsKey(entityId))
			destructedEntities[entityId] += count;
		else destructedEntities.Add(entityId, count);
	}
	public void Reset()
	{

	}
	public int GetCount(EntityId entityId)
	{
		return activeEntities.TryGetValue(entityId, out int count) ? count : 0;
	}

	public void Initialize()
	{
		pool.EntitySpawned
			.Subscribe(_ => AddActiveEntity(_.Id, _.Destructible, _.Count))
			.AddTo(disp);
		pool.EntityDespawned
			.Subscribe(_ => RemoveActiveEntity(_.Id, _.Destructible, _.Count))
			.AddTo(disp);

		foreach (var id in Enum.GetValues(typeof(EntityId)))
		{
			if ((EntityId)id == EntityId.None) continue;

			EntityConfig containConfig = configProvider.GetEntity((EntityId)id);
			if (containConfig)
			{
				activeEntities.Add(containConfig.Id, 0);
				destructedEntities.Add(containConfig.Id, 0);
			}
		}
	}

	public void Dispose()
	{
		disp.Dispose();
	}
}
