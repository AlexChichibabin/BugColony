using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IGameFactory
{
	UniTask<GameObject> CreateNewAsync(AssetReferenceGameObject prefabReference, Vector3 position, Quaternion rotation, CancellationToken token);
}