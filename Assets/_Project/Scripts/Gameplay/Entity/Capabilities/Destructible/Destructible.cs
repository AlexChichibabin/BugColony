using System;
using UniRx;
using UnityEngine;

public class Destructible : MonoBehaviour, IDestructible, ISpawnable
{
	public GameObject GameObject => gameObject;
	public IObservable<int> OnDamageTaken => onDamageTaken;
	public IObservable<Unit> OnDeath => onDeath;
	public int MaxHits => maxHits;
	public int CurrentHits => currentHits;
	public IEntityComponentRoot Root => root;


	private Subject<Unit> onDeath = new();
	private Subject<int> onDamageTaken = new();

    private bool isDeath = false;
    private int maxHits;
    private int currentHits;
	private IEntityComponentRoot root;

	public void HealFull() => currentHits = maxHits;
	public void SetHitPoints(int hitPoints) => currentHits = Mathf.Clamp(hitPoints, 0, maxHits);
	public IDestructible ApplyDamage(int damage, IDestructible other)
	{
		if (isDeath == true) return null;
		if (damage <= 0) return null;

		currentHits -= damage;
		currentHits = Mathf.Clamp(currentHits, 0, maxHits);

		onDamageTaken?.OnNext(damage);

		if (currentHits <= 0)
		{
			isDeath = true;
			Die();
			return this;
		}
		return null;
	}
	public void KillSelf()
	{
		onDamageTaken.OnNext(currentHits);
		currentHits = 0;

		Die();
	}
	public void ApplyHeal(int health)
	{
		currentHits += health;
		currentHits = Mathf.Clamp(currentHits, 0, maxHits);
	}
	public void Initialize(IEntityComponentRoot value)
	{
		root = value;
		maxHits = root.Config.Health;
	}

    protected virtual void Die()
    {
		isDeath = true;

        onDeath?.OnNext(Unit.Default);
    }

	public void OnSpawned()
	{
		HealFull();
		isDeath = false;
	}

	public void OnDespawned()
	{
		
	}
}
