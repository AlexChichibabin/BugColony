using UniRx;

public interface IFeedable : ICapability
{
	void AddFood(int food);
	void ResetFood();
    IReactiveProperty<int> EatenCount { get; }
}