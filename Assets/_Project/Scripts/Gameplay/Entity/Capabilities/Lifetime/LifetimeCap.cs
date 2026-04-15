using UniRx;
using UnityEngine;

public class LifetimeCap : MonoBehaviour, ILifetimeCap, ISpawnable
{
    public float Lifetime => lifetime;
    public IEntityComponentRoot Root => root;

    private float lifetime = -1;

    private IEntityComponentRoot root;
    private IPoolable poolable;
    private CompositeDisposable disp = new();

    public void Initialize(IEntityComponentRoot value)
    {
        root = value;
        root.TryGetCapability(out poolable);
        lifetime = root.Config.Lifetime;
    }
    public void RefreshTimer()
    {
        disp.Clear();
        StartTimer();
	}
    public void OnSpawned()
    {
        StartTimer();
    }
    public void OnDespawned()
    {
        disp?.Clear();
    }
    private void OnDisable()
    {
        disp?.Clear();
    }
    private void OnDestroy()
    {
        disp?.Dispose();
    }
    private void StartTimer()
    {
        if (lifetime > 0)
            Observable.Timer(System.TimeSpan.FromSeconds(lifetime))
                .Subscribe(_ =>
                {
                  poolable?.Despawn();
                }).
                AddTo(disp);
    }
}