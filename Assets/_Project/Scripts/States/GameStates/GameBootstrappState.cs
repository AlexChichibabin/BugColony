using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class GameBootstrappState : IEnterableState
{
    private IGameStateSwitcher gameStateSwitcher;
    private IConfigProvider configProvider;

	public GameBootstrappState(
        IGameStateSwitcher gameStateSwitcher, 
        IConfigProvider configProvider)
    {
        this.gameStateSwitcher = gameStateSwitcher;
        this.configProvider = configProvider;
    }

    public void Enter()
    {
		InitAsync().Forget();
	}
    private async UniTaskVoid InitAsync()
    {
		Debug.Log("GLOBAL: Init");

		Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.numerator;

		await Addressables.InitializeAsync().ToUniTask();

		configProvider.Load();

		var sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == Constants.BootstrappSceneName || sceneName == Constants.GameplaySceneName)
			gameStateSwitcher.Enter<LoadNextLevelState>();
	}
}