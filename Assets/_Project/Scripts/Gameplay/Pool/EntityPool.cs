using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

public class EntityPool : IEntityPool
{
	public IObservable<EntityTrackerDTO> EntitySpawned => entitySpawned;
	public IObservable<EntityTrackerDTO> EntityDespawned => entityDespawned;

	private readonly PoolContainer container;
	private readonly IConfigProvider configProvider;
    //private readonly IGameFactory gameFactory;
	private readonly IEntityStrategiesTracker tracker;

    private readonly Dictionary<EntityId, List<IPoolable>> allObjects = new();
	private readonly Dictionary<EntityId, Stack<IPoolable>> availableObjects = new();

	private Subject<EntityTrackerDTO> entitySpawned = new(); 
	private Subject<EntityTrackerDTO> entityDespawned = new();

	public EntityPool(
		IConfigProvider configProvider,
		//IGameFactory gameFactory,
		PoolContainer container,
        IEntityStrategiesTracker tracker)
	{
		this.configProvider = configProvider;
		//this.gameFactory = gameFactory;
		this.container = container;
		this.tracker = tracker;
	}

	public async UniTask PrewarmAsync(EntityId id,int count, Vector3 pos, Quaternion rot, CancellationToken token)
	{
		for (int i = 0; i < count; i++)
		{
			await CreateNewAsync(id, pos, rot, token);
		}
	}

	public async UniTask<IPoolable> SpawnAsync(EntityId id, Vector3 pos, Quaternion rot, CancellationToken token)
	{
		if (GetAvailableObjectsById(id).Count == 0)
			await CreateNewAsync(id, pos, rot, token);

		IPoolable plb = availableObjects[id].Pop();
		GameObject obj = plb.GameObject;
		obj.transform.SetPositionAndRotation(pos, rot);

		obj.SetActive(true);

		plb.OnSpawned();
		plb.Root.TryGetCapability(out IDestructible dest);
		entitySpawned.OnNext(new EntityTrackerDTO(id, 1, dest));

		return plb;
	}

	public void Despawn(EntityId id, IPoolable plb)
	{
		if (plb == null) return;

		plb.OnDespawned();

		plb.GameObject.SetActive(false);
		plb.GameObject.transform.SetParent(container.gameObject.transform);
		availableObjects[id].Push(plb);

		plb.Root.TryGetCapability(out IDestructible dest);
		entityDespawned.OnNext(new EntityTrackerDTO(id, 1, dest));
	}

	public void ReleaseAll()
	{
		allObjects.Clear();
		availableObjects.Clear();
	}


	private async UniTask<IPoolable> CreateNewAsync(EntityId id, Vector3 position, Quaternion rotation, CancellationToken token)
	{
		EntityConfig config = configProvider.GetEntity(id);

		if (!tracker.EntityFactories.ContainsKey(id)) return null;

        GameObject go = await tracker.EntityFactories[id].CreateEntity(position, rotation, token);
        //GameObject go = await gameFactory.CreateNewAsync(config.PrefabReference, position, rotation, token);

        go.transform.SetParent(container.transform);
		IPoolable plb = go.GetComponent<IPoolable>();
		//go.GetComponent<IEntityComponentRoot>().Initialize();
		go.SetActive(false);
		GetAllObjectsById(id).Add(plb);
		availableObjects[id].Push(plb);

		return plb;
	}
	private List<IPoolable> GetAllObjectsById(EntityId id)
	{
		if (!allObjects.ContainsKey(id))
		{
			allObjects.Add(id, new());
			availableObjects.Add(id, new());
		}
		return allObjects[id];
	}
	private Stack<IPoolable> GetAvailableObjectsById(EntityId id)
	{
		if (!availableObjects.ContainsKey(id))
		{
			allObjects.Add(id, new());
			availableObjects.Add(id, new());
		}
		return availableObjects[id];
	}
}
