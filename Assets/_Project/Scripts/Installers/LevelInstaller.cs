using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private LevelStateMachineTicker levelStateMachineTicker;
    [SerializeField] private EntitySpawner bugSpawner;
    [SerializeField] private CubeAreaForNavMesh spawnZone;
    [SerializeField] private PoolContainer poolContainer;

    public override void InstallBindings()
    {
        Debug.Log("LEVEL: Install");

        RegisterGameplayServices();
        RegisterSplitRules();

		RegisterLevelStateMachine();

        Container.Bind<IInitializable>().To<LevelBootstrapper>().AsSingle().NonLazy();
    }


	private void OnDestroy()
    {
        UnregisterLevelStateMachine();
    }


	private void RegisterLevelStateMachine()
    {
        Container.Bind<ILevelStateSwitcher>().To<LevelStateMachine>().AsSingle();
        Container.Bind<LevelBootstrappState>().FromNew().AsSingle();
		Container.Bind<LevelGameplayState>().FromNew().AsSingle();
        Container.Bind<LevelStateMachineTicker>().FromInstance(levelStateMachineTicker).AsSingle(); // ??
    }

    private void UnregisterLevelStateMachine()
    {
        Container.Unbind<ILevelStateSwitcher>();
		Container.Unbind<LevelBootstrappState>();
		Container.Unbind<LevelGameplayState>();
        Container.Unbind<LevelStateMachineTicker>();
    }

    private void RegisterGameplayServices()
    {
        Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        Container.Bind<IEntityPool>().To<EntityPool>().AsSingle().NonLazy();
        Container.Bind<IEntitySpawner>().FromInstance(bugSpawner).AsSingle();
        Container.Bind<ITargetResolver>().To<TargetResolver>().AsSingle();
        Container.Bind<IEntityTracker>().To<EntityTracker>().AsSingle();
        Container.Bind<ICubeArea>().FromInstance(spawnZone).AsSingle();
        Container.Bind<PoolContainer>().FromInstance(poolContainer).AsSingle();
    }
    private void RegisterSplitRules()
    {
        Container.Bind<IEntityRuleTracker>().To<EntityRuleTracker>().AsSingle();
        Container.Bind<WorkerSpawnRunner>().FromNew().AsSingle();
        Container.Bind<PredatorSpawnRunner>().FromNew().AsSingle();
		Container.Bind<FoodSpawnRunner>().FromNew().AsSingle();
	}

}
