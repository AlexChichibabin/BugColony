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
        RegisterStratigies();
        RegisterFactories();

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
        Container.Bind<LevelBootstrapState>().FromNew().AsSingle();
		Container.Bind<LevelGameplayState>().FromNew().AsSingle();
        Container.Bind<LevelStateMachineTicker>().FromInstance(levelStateMachineTicker).AsSingle(); // ??
    }

    private void UnregisterLevelStateMachine()
    {
        Container.Unbind<ILevelStateSwitcher>();
		Container.Unbind<LevelBootstrapState>();
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
        Container.Bind<IEntityStrategiesTracker>().To<EntityStrategiesTracker>().AsSingle();
        Container.Bind<IEntityRule>().To<WorkerSpawnRunner>().AsSingle();
        Container.Bind<IEntityRule>().To<PredatorSpawnRunner>().AsSingle();
        Container.Bind<IEntityRule>().To<FoodSpawnRunner>().AsSingle();
    }
    private void RegisterStratigies()
    {
        Container.Bind<ITargetingStrategy>().To<NearestTargetStrategy>().AsSingle();
        Container.Bind<ITargetingStrategy>().To<WeakestTargetStrategy>().AsSingle();
    }
    private void RegisterFactories()
    {
        Container.Bind<IEntityFactory>().To<PredatorFactory>().AsSingle();
        Container.Bind<IEntityFactory>().To<WorkerFactory>().AsSingle();
        Container.Bind<IEntityFactory>().To<MushroomFactory>().AsSingle();

    }
}
