using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class WorkerFactory : IEntityFactory
{
    public EntityId Id => EntityId.AntWorker;


    private IGameFactory gameFactory;
    private EntityConfig config;
    private IEntityRulesProvider stratProvider;

    public WorkerFactory(
        IGameFactory gameFactory,
        IEntityRulesProvider tracker,
        IConfigProvider configProvider)
    {
        this.gameFactory = gameFactory;
        this.stratProvider = tracker;
        config = configProvider.GetEntity(Id);
    }

    public async UniTask<GameObject> CreateEntity(Vector3 position, Quaternion rotation, CancellationToken token)
    {
        GameObject go = await gameFactory.CreateNewAsync(config.PrefabReference, position, rotation, token);

        IEntityComponentRoot root = go.GetComponent<IEntityComponentRoot>();
        root.Initialize();
        //if (root.TryGetCapability(out IControllerAI controller))
        //{
        //    if (stratProvider.TargetingStrategy.ContainsKey(config.TargetingStrategy))
        //        controller.SetStrategy(stratProvider.TargetingStrategy[TargetingStrategyType.FindNearest]);
        //}

        return go;
    }
}
