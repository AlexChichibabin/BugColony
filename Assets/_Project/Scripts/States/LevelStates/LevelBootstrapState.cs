using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBootstrapState : IEnterableState
{
    private ILevelStateSwitcher levelStateSwitcher;
    private IConfigProvider configProvider;
    private IEntityPool pool;
    private IEntityTracker tracker;
    private IEntitySpawner entitySpawner;
    private IEntityRuleTracker ruleRunner;
	private CancellationTokenSource cts = new();

	public LevelBootstrapState( 
        ILevelStateSwitcher levelStateSwitcher,
        IConfigProvider configProvider,
        IEntityPool pool,
        IEntitySpawner entitySpawner,
        IEntityTracker tracker,
        IEntityRuleTracker ruleRunner
        
		)
    {
        this.levelStateSwitcher = levelStateSwitcher;
        this.configProvider = configProvider;
        this.pool = pool;
        this.entitySpawner = entitySpawner;
        this.tracker = tracker;
        this.ruleRunner = ruleRunner;
    }

    public async void Enter()
    {
        Debug.Log("LEVEL: Init");

        string sceneName = SceneManager.GetActiveScene().name;
        LevelConfig levelConfig = configProvider.GetLevel(sceneName);
		tracker.Initialize();
		ruleRunner.CreateRules();
		await PrewarmPool();

		levelStateSwitcher.Enter<LevelGameplayState>();
	}

    private async UniTask PrewarmPool()
    {
		await pool.PrewarmAsync(EntityId.AntWorker, 20, entitySpawner.SpawnPoint.position, entitySpawner.SpawnPoint.rotation, cts.Token);
        await pool.PrewarmAsync(EntityId.BugPredator, 10, entitySpawner.SpawnPoint.position, entitySpawner.SpawnPoint.rotation, cts.Token);
		await pool.PrewarmAsync(EntityId.Mushroom, 50, Vector3.zero, Quaternion.identity, cts.Token);
	}
}