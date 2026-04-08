using System.Collections.Generic;

public interface IEntityRuleTracker
{
	IReadOnlyDictionary<EntityId, IEntityRule> EntityRules { get; }
	void Initialize();
	void CreateRules();
}