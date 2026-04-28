using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private LevelStateMachineTicker levelStateMachineTicker;
    [SerializeField] private EntitySpawner entitySpawner;
    [SerializeField] private CubeAreaForNavMesh spawnZone;
    [SerializeField] private PoolContainer poolContainer;

    public override void InstallBindings()
    {
        Debug.Log("LEVEL: Install");

        RegisterGameplayServices();
        RegisterSpawnRules();
        RegisterStrategies();
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
        Container.Bind<IEntityPool>().To<EntityPool>().AsSingle();
        Container.Bind<IEntitySpawner>().FromInstance(entitySpawner).AsSingle();
        Container.Bind<ITargetResolver>().To<TargetResolver>().AsSingle();
        Container.Bind<IEntityTracker>().To<EntityTracker>().AsSingle();
        Container.Bind<ICubeArea>().FromInstance(spawnZone).AsSingle();
        Container.Bind<PoolContainer>().FromInstance(poolContainer).AsSingle();
    }
    private void RegisterSpawnRules()
    {
        Container.BindInterfacesAndSelfTo<EntityRuleProvider>().AsSingle();

        Container.BindInterfacesAndSelfTo<WorkerSpawnRunner>().AsSingle();
        Container.BindInterfacesAndSelfTo<PredatorSpawnRunner>().AsSingle();
        Container.BindInterfacesAndSelfTo<FoodSpawnRunner>().AsSingle();
    }
    private void RegisterStrategies()
    {
        Container.Bind<IStrategyProvider>().To<StrategyProvider>().AsSingle();

        Container.Bind<ITargetingStrategy>().To<NearestTargetStrategy>().AsSingle();
        Container.Bind<ITargetingStrategy>().To<WeakestTargetStrategy>().AsSingle();
    }
    private void RegisterFactories()
    {
        Container.Bind<IEntityFactoryProvider>().To<EntityFactoryProvider>().AsSingle();
        Container.Bind<IEntityFactory>().To<PredatorFactory>().AsSingle();
        Container.Bind<IEntityFactory>().To<WorkerFactory>().AsSingle();
        Container.Bind<IEntityFactory>().To<MushroomFactory>().AsSingle();
    }
}
