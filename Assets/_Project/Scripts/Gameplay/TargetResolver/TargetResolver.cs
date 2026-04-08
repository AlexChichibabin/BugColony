using UnityEngine;

public class TargetResolver : ITargetResolver
{
	public IDestructible Resolve(TargetContext ctx)
	{
		if (ctx.Candidates == null || ctx.Candidates.Count == 0) return null;
		IDestructible best = null;

		float bestDist = float.PositiveInfinity;

		foreach (var target in ctx.Candidates)
		{
			if (target == null) continue;
			if (target.GameObject.Equals(ctx.Actor)) continue;
			if (!ctx.ActorConfig.TryGetTargetRule(ctx.ActorConfig.Id, out var rule)) continue;
			if (rule.TargetId == EntityId.None || rule.TargetId == target.Root.Id)
			{ 
				float d = Vector3.Distance(target.GameObject.transform.position, ctx.Actor.transform.position);
				bool better = d < bestDist;
				if (!better) continue;
				best = target;
				bestDist = d;
			}
		}

		return best;
	}
}





