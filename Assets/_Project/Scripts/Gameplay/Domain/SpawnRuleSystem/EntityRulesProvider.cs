using System;
using System.Collections.Generic;
using System.Linq;

public class EntityRulesProvider : IEntityRulesProvider, IDisposable
{
	public IReadOnlyDictionary<EntityId, IEntityRule> EntityRules => rules;
    public IReadOnlyDictionary<TargetingStrategyType, ITargetingStrategy> TargetingStrategy => targetings;

    private Dictionary<EntityId, IEntityRule> rules = new();
    private Dictionary<TargetingStrategyType, ITargetingStrategy> targetings = new();

    public EntityRulesProvider(
		List<IEntityRule> rules,
		List<ITargetingStrategy> targetings
		)
	{
		this.rules = rules.ToDictionary(x => x.Id, x => x);
        this.targetings = targetings.ToDictionary(x => x.Type, x => x);
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
}