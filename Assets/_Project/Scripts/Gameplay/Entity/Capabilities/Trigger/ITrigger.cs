using System.Collections.Generic;

public interface ITrigger : ICapability
{
	IList<IDestructible> Candidates { get; }
	void OnSpawned();
	void OnDespawned();
}