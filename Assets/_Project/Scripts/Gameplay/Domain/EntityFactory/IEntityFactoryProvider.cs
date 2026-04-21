using System.Collections.Generic;
using UnityEngine;

public interface IEntityFactoryProvider
{
    IReadOnlyDictionary<EntityId, IEntityFactory> Factories { get; }
}
