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
	public EntityTypeFlags TypeFlags => typeFlags;
	public EntityId Id => id;
	public string Title => title;
	public AssetReferenceGameObject PrefabReference => prefabReference;
	public int FoodValue => foodValue;
	public int Health => health;
	public float Lifetime => lifetime;
    public TargetingStrategyType TargetingStrategy => targetingStrategyType;

    [Serializable]
	public struct TargetRule
	{
		public EntityTypeFlags TargetFlags;
	}

	[Header("Spawn")]
	[SerializeField] private EntityId id;

	[Header("Entity")]
	[SerializeField] private string title;
	[SerializeField] private AssetReferenceGameObject prefabReference;

	[Header("Food")]
	[SerializeField] private int foodValue;

	[Header("Characteristics")]
	[SerializeField] private int health;
	[SerializeField] private float movementSpeed;
	[SerializeField] private int attackDamage;
	[SerializeField] private float attackRate;
    [SerializeField] private float lifetime = -1f;
    [SerializeField] private EntityTypeFlags typeFlags;
	[SerializeField] private TargetingStrategyType targetingStrategyType;


	[Header("AI")]
	[SerializeField] private float stopDistance;
	[SerializeField] private float attackDistance;

	[SerializeField] private TargetRule rule;

	public bool TryGetTargetRule(out TargetRule rule)
	{
		rule = this.rule;
		return true;
	}
}

