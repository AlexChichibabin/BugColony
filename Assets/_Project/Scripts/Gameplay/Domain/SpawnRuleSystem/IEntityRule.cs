using System;

public interface IEntityRule : IDisposable
{
	EntityId Id { get; }
	void Initialize();
	void SubscribeOnEntity(ISplitable splitable);
}
