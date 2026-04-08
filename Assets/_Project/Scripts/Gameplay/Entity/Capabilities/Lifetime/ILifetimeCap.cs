public interface ILifetimeCap : ICapability
{
    float Lifetime { get; }
	void RefreshTimer();
}