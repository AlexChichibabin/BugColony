using System;
using UniRx;
using UnityEngine;

public interface IDestructible : ICapability
{
    GameObject GameObject { get; }
	public int MaxHits {  get; }
	public int CurrentHits { get; }
	IObservable<int> OnDamageTaken { get; }
	IObservable<Unit> OnDeath { get; }

	void HealFull();
	void SetHitPoints(int hitPoints);
	IDestructible ApplyDamage(int damage, IDestructible other);
	void KillSelf();
	void ApplyHeal(int health);
	void OnSpawned();
	void OnDespawned();
}
