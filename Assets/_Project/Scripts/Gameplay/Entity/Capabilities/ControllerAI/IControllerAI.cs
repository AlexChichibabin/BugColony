public interface IControllerAI : ICapability
{
    void SetStrategy(ITargetSelectionStrategy strategy);
}