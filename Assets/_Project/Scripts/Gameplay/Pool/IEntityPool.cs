using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public interface IEntityPool
{
	UniTask<IPoolable> SpawnAsync(EntityId id, Vector3 position, Quaternion rotation, CancellationToken token);
	void Despawn(EntityId id, IPoolable entity);
	UniTask PrewarmAsync(EntityId id, int count, Vector3 pos, Quaternion rot, CancellationToken token);
	void ReleaseAll();
	IObservable<EntityTrackerDTO> EntitySpawned {  get; }
	IObservable<EntityTrackerDTO> EntityDespawned { get; }
}
