using UnityEngine;
using Zenject;

public class LevelBootstrapper : IInitializable
{
    private ILevelStateSwitcher levelStateSwitcher;
    private LevelBootstrappState levelBootstrappState;
    private LevelGameplayState levelResearchState;

    public LevelBootstrapper(
        ILevelStateSwitcher levelStateSwitcher,
        LevelBootstrappState levelBootstrappState,
        LevelGameplayState levelResearchState
        )
    {
        this.levelStateSwitcher = levelStateSwitcher;
        this.levelBootstrappState = levelBootstrappState;
        this.levelResearchState = levelResearchState;
    }

    public void Initialize()
    {
        Debug.Log("LEVEL: Boot");
        InitLevelStateMachine();
    }

    private void InitLevelStateMachine()
    {
        levelStateSwitcher.AddState(levelBootstrappState);
        levelStateSwitcher.AddState(levelResearchState);

        levelStateSwitcher.Enter<LevelBootstrappState>();
    }
}