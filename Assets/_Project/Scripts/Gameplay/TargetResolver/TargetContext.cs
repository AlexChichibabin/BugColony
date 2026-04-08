using System.Collections.Generic;
using UnityEngine;

public struct TargetContext
{
    public GameObject Actor;
    public IList<IDestructible> Candidates;
    public EntityConfig ActorConfig;

    public TargetContext(
        GameObject actor, 
		IList<IDestructible> candidates,
        EntityConfig actorConfig)
    {
        Actor = actor;
		Candidates = candidates;
		ActorConfig = actorConfig;
	}
}
