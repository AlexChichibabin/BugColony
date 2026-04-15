public sealed class NearestTargetStrategy : ITargetSelectionStrategy
{
    
    private ITargetResolver resolver;

    public NearestTargetStrategy(ITargetResolver resolver)
    {
        this.resolver = resolver;
    }

    public IDestructible Select(TargetContext context)
    {
        return resolver.Resolve(context);
    }
}