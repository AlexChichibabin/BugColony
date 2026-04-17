using System;
using System.Collections.Generic;
using System.Linq;

public class EntityStrategiesTracker : IEntityStrategiesTracker, IDisposable
{
	public IReadOnlyDictionary<EntityId, IEntityRule> EntityRules => rules;
    public IReadOnlyDictionary<EntityId, IEntityFactory> EntityFactories => factories;
    public IReadOnlyDictionary<TargetingStrategyType, ITargetingStrategy> TargetingStrategy => targetings;


    private Dictionary<EntityId, IEntityRule> rules = new();
    private Dictionary<EntityId, IEntityFactory> factories = new();
    private Dictionary<TargetingStrategyType, ITargetingStrategy> targetings = new();



    public EntityStrategiesTracker(
		List<IEntityRule> rules,
		List<ITargetingStrategy> targetings,
		List<IEntityFactory> factories)
	{
		this.rules = rules.ToDictionary(x => x.Id);
        this.factories = factories.ToDictionary(x => x.Id);
        this.targetings = targetings.ToDictionary(x => x.Type);
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