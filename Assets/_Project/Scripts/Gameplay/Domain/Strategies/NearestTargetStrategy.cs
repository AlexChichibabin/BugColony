public sealed class NearestTargetStrategy : ITargetingStrategy
{
    public TargetingStrategyType Type => TargetingStrategyType.FindNearest;


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