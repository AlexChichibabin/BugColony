using System;
using System.Collections.Generic;

public class EntityRuleTracker : IEntityRuleTracker, IDisposable
{
	public IReadOnlyDictionary<EntityId, IEntityRule> EntityRules => rules;

	private Dictionary<EntityId, IEntityRule> rules = new();

	private WorkerSpawnRunner workerRunner;
	private PredatorSpawnRunner predatorRunner;
	private FoodSpawnRunner foodRunner;

	public EntityRuleTracker(
		WorkerSpawnRunner workerRule,
		PredatorSpawnRunner predatorRule,
		FoodSpawnRunner foodSpawnRunner)
	{
		this.workerRunner = workerRule;
		this.predatorRunner = predatorRule;
		this.foodRunner = foodSpawnRunner;
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
		rules.Add(EntityId.AntWorker, workerRunner);
		rules.Add(EntityId.BugPredator, predatorRunner);
		rules.Add(EntityId.Food, foodRunner);
	}
}