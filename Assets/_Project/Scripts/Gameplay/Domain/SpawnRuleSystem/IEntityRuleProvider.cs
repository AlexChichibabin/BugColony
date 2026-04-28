using System;
using System.Collections.Generic;

public interface IEntityRuleProvider : IDisposable
{
	IReadOnlyDictionary<EntityId, IEntityRule> EntityRules { get; }

    void Initialize();
}