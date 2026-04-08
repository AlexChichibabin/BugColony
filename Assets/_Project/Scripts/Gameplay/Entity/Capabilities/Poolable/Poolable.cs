using System;
using UniRx;
using UnityEngine;
using Zenject;

public class Poolable : MonoBehaviour, IPoolable, IDisposable
{
	public GameObject GameObject => gameObject;
	public IEntityComponentRoot Root => root;

	private IEntityPool pool;
	private IEntityComponentRoot root;
	private IDestructible destructible;
	private IControllerAI controllerAI;
	private ITrigger trigger;

	private CompositeDisposable disp = new();

	[Inject]
	public void Construct(
		IEntityPool pool)
	{
		this.pool = pool;
	}
	public void OnSpawned()
	{
		if (controllerAI != null) controllerAI.OnSpawned();
		if (trigger != null) trigger.OnSpawned();
		if (destructible != null) destructible.OnSpawned();
	}
	public void OnDespawned()
	{
		if (controllerAI != null) controllerAI.OnDespawned();
		if (trigger != null) trigger.OnDespawned();
		if (destructible != null) destructible.OnDespawned();
	}
	public void Despawn()
	{
		pool.Despawn(root.Id, this);
	}

	public void Initialize(IEntityComponentRoot value)
	{
		root = value;
		root.TryGetCapability(out destructible);
		root.TryGetCapability(out controllerAI);
		root.TryGetCapability(out trigger);
		destructible.OnDeath.Subscribe(_ => Despawn()).AddTo(disp);
	}

	public void Dispose()
	{
		disp.Dispose();
	}
}
