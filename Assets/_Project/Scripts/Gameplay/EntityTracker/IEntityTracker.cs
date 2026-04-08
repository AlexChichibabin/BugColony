using UniRx;

public interface IEntityTracker
{
	IReadOnlyReactiveDictionary<EntityId, int> ActiveEntities { get; }
	IReadOnlyReactiveDictionary<EntityId, int> DestructedEntities { get; }
	IReactiveCollection<IDestructible> AllActiveEntities { get; }


	int GetCount(EntityId entityId);
	void Initialize();
	void Reset();
}