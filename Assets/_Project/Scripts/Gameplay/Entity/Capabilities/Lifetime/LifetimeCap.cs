using UniRx;
using UnityEngine;

public class LifetimeCap : MonoBehaviour, ILifetimeCap
{
    public float Lifetime => lifetime;
    public IEntityComponentRoot Root => root;

    [SerializeField] private float lifetime = 10f;

    private IEntityComponentRoot root;
    private IPoolable poolable;
    private CompositeDisposable disp = new();

    public void Initialize(IEntityComponentRoot value)
    {
        root = value;
        root.TryGetCapability(out poolable);
    }
    public void RefreshTimer()
    {
        disp.Clear();
        StartTimer();
	}
    private void OnEnable()
    {
        StartTimer();
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
		Observable.Timer(System.TimeSpan.FromSeconds(lifetime))
			.Subscribe(_ =>
			{
				poolable?.Despawn();
			}).
			AddTo(disp);
	}
}