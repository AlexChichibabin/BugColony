public interface ITargetingStrategy
{
    TargetingStrategyType Type { get; }
    IDestructible Select(TargetContext context);
}
