using UnityEngine;
using Zenject;

public class LevelGameplayState : IEnterableState, ITickableState, IExitableState
{
	[Inject] private IEntityStrategiesTracker entityRuleRunner;

    public void Enter()
    {
        Debug.Log("LEVEL: Gameplay");

		entityRuleRunner.Initialize();
	}
    public void Exit() { }

    public void Tick() { }

}