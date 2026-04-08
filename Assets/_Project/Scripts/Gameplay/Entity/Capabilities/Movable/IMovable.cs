using UnityEngine;

public interface IMovable : ICapability
{
	void StartMoving(Transform target);
	void StopMoving();
}
