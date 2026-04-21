using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public interface IEntityFactory
{
    EntityId Id { get; }
    UniTask<GameObject> CreateEntity(Vector3 position, Quaternion rotation, CancellationToken token);
}
