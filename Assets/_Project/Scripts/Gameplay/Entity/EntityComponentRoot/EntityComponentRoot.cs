using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EntityComponentRoot : MonoBehaviour, IEntityComponentRoot
{
	public IReadOnlyDictionary<Type, object> Capabilities => capabilities;
	public GameObject GameObject => gameObject;
	public EntityId Id => id;
	public EntityConfig Config => config;

	[SerializeField] protected EntityId id;
	[SerializeField] private EntityConfig config;

	protected readonly Dictionary<Type, object> capabilities = new();

	public virtual void Initialize()
	{
		foreach (var comp in GetComponents<MonoBehaviour>())
		{
            if (comp is not ICapability || comp is IEntityComponentRoot) continue;

            var type = comp.GetType();

			foreach (var i in type.GetInterfaces())
				if (typeof(ICapability).IsAssignableFrom(i) && i != typeof(ICapability))
					capabilities[i] = comp;
		}

		foreach (var comp in capabilities.Values)
			(comp as ICapability).Initialize(this);
	}
	public bool TryGetCapability<T>(out T cap) where T : class
	{
		if (capabilities.TryGetValue(typeof(T), out var obj))
		{
			cap = obj as T;
			return cap != null;
		}
		cap = null;
		return false;
	}
}
