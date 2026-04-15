using System;
using System.Collections.Generic;
using UniRx;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;


public class Poolable : MonoBehaviour, IPoolable, IDisposable
{
	public GameObject GameObject => gameObject;
	public IEntityComponentRoot Root => root;

	private IEntityPool pool;
	private IEntityComponentRoot root;
	private IDestructible destructible;
    private ISpawnable[] spawnables;
    private readonly CompositeDisposable disp = new();

	[Inject]
	public void Construct(
		IEntityPool pool)
	{
		this.pool = pool;
	}
	public void OnSpawned()
	{
		foreach (var cap in spawnables)
		{
			cap.OnSpawned();
		}
	}
	public void OnDespawned()
	{
        foreach (var cap in spawnables)
        {
            cap.OnDespawned();
        }
    }
	public void Despawn()
	{
		pool.Despawn(root.Config.Id, this);
	}

	public void Initialize(IEntityComponentRoot value)
	{
		root = value;
		root.TryGetCapabilitiesByType(out spawnables);

        root.TryGetCapability(out destructible);

		destructible.OnDeath.Subscribe(_ => Despawn()).AddTo(disp);
	}

	public void Dispose()
	{
		disp.Dispose();
	}
}
