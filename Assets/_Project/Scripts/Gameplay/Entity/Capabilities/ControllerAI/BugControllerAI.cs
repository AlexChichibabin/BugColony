using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class BugControllerAI : MonoBehaviour, IControllerAI, ISpawnable
{
	public IEntityComponentRoot Root => root;

	[Inject] private NearestTargetStrategy strategy; // must be interface
	private IEntityComponentRoot root;
	private ReactiveProperty<IDestructible> target = new();

	private IMovable movable;
	private IAttack attack;
	private ITrigger trigger;

	private CompositeDisposable disp = new();

	public void Initialize(IEntityComponentRoot value)
	{
		root = value;

		root.TryGetCapability(out movable);
		root.TryGetCapability(out attack);
		root.TryGetCapability(out trigger);
	}

	public void OnSpawned()
	{
		ActivateTargeting();
		ActivateMove();
		ActivateAttack();
	}

	public void OnDespawned()
	{
		if (disp != null)
			disp?.Clear();
	}
    public void SetStrategy(ITargetSelectionStrategy strategy)
    {
        //this.strategy = strategy;
    }
    private void ActivateTargeting()
	{
		target
			.Subscribe(tgt =>
			{
				if (tgt != null)
				{
					if (tgt.GameObject.activeSelf == false)
						tgt = null;
				}
				else if (trigger != null) FindTarget(trigger.Candidates);
			})
			.AddTo(disp);

		Observable
			.Interval(TimeSpan.FromSeconds(1))
			.Subscribe(_ => FindTarget(trigger.Candidates))
			.AddTo(disp);
	}
	private void ActivateMove()
	{
		Observable
			.EveryUpdate()
			.Where(_ => target.Value != null && movable != null)
			.Subscribe(_ =>
			{
				if (CheckStopDistance())
					movable.StopMoving();
				else movable.StartMoving(target.Value.GameObject.transform);
			})
			.AddTo(disp);
	}
	private void ActivateAttack()
	{
		Observable
			.EveryUpdate()
			.Where(_ => target.Value != null && attack != null && CheckAtkDistance())
			.ThrottleFirst(TimeSpan.FromSeconds(root.Config.AttackRate))
			.Subscribe(_ =>
			{
				if (attack.Attack(target.Value) != null)
					target.Value = null;
			})
			.AddTo(disp);
	}
	private bool CheckAtkDistance()
	{
		return Vector3.Distance(transform.position, target.Value.GameObject.transform.position) < root.Config.AttackDistance;
	}
	private bool CheckStopDistance()
	{
		return Vector3.Distance(transform.position, target.Value.GameObject.transform.position) < root.Config.StopDistance;
	}
	private void FindTarget(IList<IDestructible> candidates)
	{
		TargetContext ctx = new TargetContext(
			actor: gameObject,
			candidates: candidates,
			actorConfig: root.Config
			);

		target.Value = strategy.Select(ctx);
	}

}
