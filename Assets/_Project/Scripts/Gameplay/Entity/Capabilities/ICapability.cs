public interface ICapability
{
    void Initialize(IEntityComponentRoot value);
	IEntityComponentRoot Root { get; }
}
