using System;
using System.Collections.Generic;

public interface IHasCapability
{
    IReadOnlyDictionary<Type, object> Capabilities { get; }
    bool TryGetCapability<T>(out T cap) where T : class;
}
