using UniRx;
using UnityEngine;

public class Feedable : MonoBehaviour, IFeedable
{
	public IEntityComponentRoot Root => root;
	public IReactiveProperty<int> EatenCount => eatedCount;

	private ReactiveProperty<int> eatedCount = new();
	private IEntityComponentRoot root;


	public void AddFood(int foodCount)
	{
		if (foodCount <= 0) return;

		eatedCount.Value += foodCount;
	}

	public void Initialize(IEntityComponentRoot value)
	{
		root = value;
	}

	public void ResetFood()
	{
		eatedCount.Value = 0;
	}

	private void OnDisable()
	{
		ResetFood();
	}
}
