using UnityEngine;

public class WeakestTargetStrategy : ITargetingStrategy
{
    public TargetingStrategyType Type => TargetingStrategyType.FindWeakest;


    private ITargetResolver resolver;
    public IDestructible Select(TargetContext context)
    {
        throw new System.NotImplementedException();
    }
}
