using UniRx;
using Zenject;

public class PredatorSpawnRunner : IEntityRule
{
    public EntityId Id => EntityId.BugPredator;

    [Inject] private readonly ICubeArea spawnArea;

	private readonly int foodEatenForSplit = 3;
	private readonly CompositeDisposable disp = new();

	public void Initialize()
	{

	}
	public void SubscribeOnEntity(ISplitable splitable)
	{
		splitable.Root.TryGetCapability(out IFeedable feedable);
		splitable.Root.TryGetCapability(out ILifetimeCap lifetime);
		feedable.EatenCount.
			Where(x => x == foodEatenForSplit).
			Subscribe(_ =>
			{
				splitable.Split(EntityId.BugPredator, 1, spawnArea.GetRandomInsideZone());
				lifetime.RefreshTimer();
			})
			.AddTo(disp);
	}

	public void Dispose()
	{
		disp.Dispose();
	}
}