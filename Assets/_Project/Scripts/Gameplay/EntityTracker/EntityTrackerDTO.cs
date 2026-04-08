public struct EntityTrackerDTO
{
	public EntityId Id;
	public IDestructible Destructible;
	public int Count;

	public EntityTrackerDTO(EntityId id, int count, IDestructible dest)
	{
		Id = id;
		Count = count;
		Destructible = dest;
	}
}
