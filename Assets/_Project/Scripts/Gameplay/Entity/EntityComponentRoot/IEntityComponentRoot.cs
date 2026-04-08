using UnityEngine;

public interface IEntityComponentRoot : IHasCapability
{
	GameObject GameObject { get; }
	EntityId Id { get; }
	EntityConfig Config { get; }
	void Initialize();
}