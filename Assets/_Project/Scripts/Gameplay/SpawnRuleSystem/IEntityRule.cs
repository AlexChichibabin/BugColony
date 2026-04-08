using System;

public interface IEntityRule : IDisposable
{
	void Initialize();
	void SubscribeOnEntity(ISplitable splitable);
}
