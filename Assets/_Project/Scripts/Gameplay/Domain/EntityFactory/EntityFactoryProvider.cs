using System.Collections.Generic;
using System.Linq;

public class EntityFactoryProvider : IEntityFactoryProvider
{
    public IReadOnlyDictionary<EntityId, IEntityFactory> Factories => factories;

    private Dictionary<EntityId, IEntityFactory> factories = new();

    public EntityFactoryProvider(
		List<IEntityFactory> factories)
    {
        this.factories = factories.ToDictionary(x => x.Id, x => x);
    }
}
