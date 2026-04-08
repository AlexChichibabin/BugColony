using UnityEngine;
using Zenject;

public class GameBootstrapper : IInitializable
{
    private IGameStateSwitcher gameStateSwitcher;
    private GameBootstrappState gameBootstrappState;
    private LoadNextLevelState loadNextLevelState;


    public GameBootstrapper(
        IGameStateSwitcher gameStateSwitcher, 
        GameBootstrappState gameBootstrappState, 
        LoadNextLevelState loadNextLevelState
        )
    {
        this.gameStateSwitcher = gameStateSwitcher;
        this.gameBootstrappState = gameBootstrappState;
        this.loadNextLevelState = loadNextLevelState;
    }

    public void Initialize()
    {
        Debug.Log("GLOBAL: Boot");
        InitGameStateMachine();
    }

    private void InitGameStateMachine()
    {
        gameStateSwitcher.AddState(gameBootstrappState);
        gameStateSwitcher.AddState(loadNextLevelState);

        gameStateSwitcher.Enter<GameBootstrappState>();
    }
}