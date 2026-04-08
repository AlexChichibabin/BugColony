using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class GameFactory : IGameFactory
{
    private DiContainer ñontainer;

    public GameFactory(
        DiContainer ñontainer)
    {
        this.ñontainer = ñontainer;
    }
    public async UniTask<GameObject> CreateNewAsync(AssetReferenceGameObject prefabReference, Vector3 position, Quaternion rotation, CancellationToken token)
    {
		GameObject go = await InstantiateAndInject(prefabReference, position, rotation, token);

		return go;
    }
	private async UniTask<GameObject> InstantiateAndInject(AssetReferenceGameObject prefabReference, Vector3 position, Quaternion rotation, CancellationToken token)
	{
		GameObject newGameObject = await prefabReference.InstantiateAsync(position, rotation).ToUniTask(cancellationToken: token);
		ñontainer.InjectGameObject(newGameObject);

		return newGameObject;
	}
}