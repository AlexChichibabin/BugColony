using UnityEngine;

public interface IPoolable : ICapability
{
    GameObject GameObject { get; }
    void OnSpawned();
    void OnDespawned();
    void Despawn();
}
