using System;
using UniRx;
using UnityEngine;
using Zenject;

public class Splitable : MonoBehaviour, ISplitable, IDisposable
{
    public IEntityComponentRoot Root => root;

	[Inject] private IEntitySpawner spawner;
	[Inject] private IEntityStrategiesTracker ruleTracker;

	private IFeedable feedable;
    private IEntityComponentRoot root;
    private IEntityRule rule;

	private CompositeDisposable disp = new();

    public void Split(EntityId id, int count, Vector3 position)
    {
		feedable.ResetFood();
		for (int i = 0; i < count; i++)
        {
			spawner.SpawnEntityRandomRotation(id, position);
		}
	}

	public void Initialize(IEntityComponentRoot value)
	{
		root = value;
		root.TryGetCapability(out feedable);

		rule = ruleTracker.EntityRules[root.Config.Id];
		rule.SubscribeOnEntity(this);
	}
	private void OnDisable()
	{
		disp.Clear();
	}

	public void Dispose()
	{
		disp.Dispose();
	}
}
