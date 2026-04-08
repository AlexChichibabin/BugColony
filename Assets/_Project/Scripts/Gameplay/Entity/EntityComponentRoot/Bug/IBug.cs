using UnityEngine;
using UnityEngine.AI;

public interface IBug : IHasCapability
{
	GameObject GameObject { get; }
    NavMeshAgent Agent { get; }
}
