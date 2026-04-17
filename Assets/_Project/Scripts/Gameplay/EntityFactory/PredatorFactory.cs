using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public sealed class PredatorFactory : IEntityFactory
{
    public EntityId Id => EntityId.BugPredator;


    private IGameFactory gameFactory;
    private EntityConfig config;
    private IEntityStrategiesTracker tracker;

    public PredatorFactory(
        IGameFactory gameFactory,
        IEntityStrategiesTracker tracker,
        IConfigProvider configProvider)
    {
        this.gameFactory = gameFactory;
        this.tracker = tracker;
        config = configProvider.GetEntity(Id);
    }


    public async UniTask<GameObject> CreateEntity(Vector3 position, Quaternion rotation, CancellationToken token)
    {
        GameObject go = await gameFactory.CreateNewAsync(config.PrefabReference, position, rotation, token);

        IEntityComponentRoot root = go.GetComponent<IEntityComponentRoot>();
        root.Initialize();
        if (root.TryGetCapability(out IControllerAI controller))
        {
            if (tracker.TargetingStrategy.ContainsKey(config.TargetingStrategy))
                controller.SetStrategy(tracker.TargetingStrategy[TargetingStrategyType.FindNearest]);
        }

        return go;
    }
}
