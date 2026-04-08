using UnityEngine;

public interface IEntitySpawner
{
	Transform SpawnPoint { get; }
	void SpawnEntityRandomRotation(EntityId id, Vector3 position);
	void SpawnEntity(EntityId id, Vector3 position, Quaternion rotation);
}