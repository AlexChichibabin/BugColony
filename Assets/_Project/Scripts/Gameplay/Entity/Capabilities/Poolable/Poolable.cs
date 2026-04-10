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
    private IReadOnlyDictionary<Type, object> capabilities;
    private readonly CompositeDisposable disp = new();

	[Inject]
	public void Construct(
		IEntityPool pool)
	{
		this.pool = pool;
	}
	public void OnSpawned()
	{
		foreach (var cap in capabilities.Values)
		{
			if (cap is ISpawnable) (cap as ISpawnable).OnSpawned();
		}
	}
	public void OnDespawned()
	{
        foreach (var cap in capabilities.Values)
        {
            if (cap is ISpawnable) (cap as ISpawnable).OnDespawned();
        }
    }
	public void Despawn()
	{
		pool.Despawn(root.Id, this);
	}

	public void Initialize(IEntityComponentRoot value)
	{
		root = value;
		capabilities = root.Capabilities;

        root.TryGetCapability(out destructible);

		destructible.OnDeath.Subscribe(_ => Despawn()).AddTo(disp);
	}

	public void Dispose()
	{
		disp.Dispose();
	}
}
