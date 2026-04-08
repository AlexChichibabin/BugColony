using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "EntityConfig", menuName = "Configs/Entity")]
public class EntityConfig : ScriptableObject
{
	public float MovementSpeed => movementSpeed;
	public int AttackDamage => attackDamage;
	public float StopDistance => stopDistance;
	public float AttackDistance => attackDistance;
	public float AttackRate => attackRate;
	public EntityId ReadyToEat => readyToEat;
	public EntityId Id => id;
	public string Title => title;
	public AssetReferenceGameObject PrefabReference => prefabReference;
	public int FoodValue => foodValue;
	public float Health => health;

	[Serializable]
	public struct TargetRule
	{
		public EntityId EntityId;
		public EntityId TargetId;
	}

	[Header("Spawn")]
	[SerializeField] private EntityId id;

	[Header("Entity")]
	[SerializeField] private string title;
	[SerializeField] private AssetReferenceGameObject prefabReference;

	[Header("Food")]
	[SerializeField] private int foodValue;

	[Header("Characteristics")]
	[SerializeField] private float health;
	[SerializeField] private float movementSpeed;
	[SerializeField] private int attackDamage;
	[SerializeField] private float attackRate;
	[SerializeField] private EntityId readyToEat;

	[Header("AI")]
	[SerializeField] private float stopDistance;
	[SerializeField] private float attackDistance;

	[SerializeField] private TargetRule[] Rules;

	public bool TryGetTargetRule(EntityId state, out TargetRule rule)
	{
		for (int i = 0; i < Rules.Length; i++)
		{
			if (Rules[i].EntityId == state)
			{
				rule = Rules[i];
				return true;
			}
		}

		for (int i = 0; i < Rules.Length; i++)
		{
			if (Rules[i].EntityId == EntityId.None)
			{
				rule = Rules[i];
				return true;
			}
		}

		rule = default;
		return false;
	}
}

