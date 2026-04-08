using UnityEngine;

public interface ISplitable : ICapability
{
	void Split(EntityId id, int count, Vector3 position);
}