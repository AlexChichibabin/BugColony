using System;
using System.Collections.Generic;
using System.Linq;

public class EntityRuleTracker : IEntityRuleTracker, IDisposable
{
	public IReadOnlyDictionary<EntityId, IEntityRule> EntityRules => rules;

	private Dictionary<EntityId, IEntityRule> rules = new();



	public EntityRuleTracker(
		List<IEntityRule> rules)
	{
		this.rules = rules.ToDictionary(x => x.Id);
	}

	public void Initialize()
	{
		foreach (var rule in rules)
		{
			rule.Value.Initialize();
		}
	}

	public void Dispose()
	{
		foreach (var rule in rules)
		{
			rule.Value.Dispose();
		}
	}
	public void CreateRules()
	{
		
	}
}