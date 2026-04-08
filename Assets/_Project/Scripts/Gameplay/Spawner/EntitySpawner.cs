using System;
using System.Threading;
using UniRx;
using UnityEngine;
using Zenject;

public class EntitySpawner : MonoBehaviour, IEntitySpawner, IDisposable
{
	public Transform SpawnPoint => spawnPoint;

	[SerializeField] private Transform poolContainer;
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private CubeArea spawnZone;

	[Inject] private IEntityPool pool;
	private CompositeDisposable disp = new();
	private CancellationTokenSource cts = new();

	public async void SpawnEntityRandomRotation(EntityId id, Vector3 position)
	{
		Quaternion randomTurn = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360) - 30, 0);
		await pool.SpawnAsync(id, position, randomTurn, cts.Token);
	}
	public async void SpawnEntity(EntityId id, Vector3 position, Quaternion rotation)
	{
		await pool.SpawnAsync(id, position, rotation, cts.Token);
	}

	private void OnDestroy()
	{
		Dispose();
	}

	public void Dispose()
	{
		disp.Dispose();
		cts.Cancel();
		cts.Dispose();
	}
}
