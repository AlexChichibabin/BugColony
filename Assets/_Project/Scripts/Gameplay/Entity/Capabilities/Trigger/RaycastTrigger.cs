using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class RaycastTrigger : TriggerBase, ITrigger
{
	public override IList<IDestructible> Candidates => GetTargetCandidates();
	public IEntityComponentRoot Root => root;


	[SerializeField] private float radius = 0.8f;
	[SerializeField] private LayerMask destructibleMask = ~0; // Все колайдеры должны быть на слое Destructible
	[SerializeField] private int maxHits = 16;
	[SerializeField] private NavMeshAgent agent; // Serialize field


	private Transform parent;
	private readonly HashSet<IDestructible> set = new();
	private Collider[] hits;
	private List<IDestructible> candidates;
	private IEntityComponentRoot root;
	private IControllerAI controllerAI;


	private CompositeDisposable disp = new();

	public void OnSpawned()
	{
		
	}

	public void OnDespawned()
	{
		disp.Clear();
	}

	private List<IDestructible> GetTargetCandidates()
	{
		set.Clear();
		if (root == null) return null;
		Vector3 downWorld = parent.TransformPoint(transform.position);
		Vector3 half = new Vector3(transform.position.x, transform.position.y + agent.height * 0.5f, transform.position.z);

		int count = Physics.OverlapSphereNonAlloc(half, radius, hits, destructibleMask);

		for (int i = 0; i < count; i++)
		{
			var collider = hits[i];
			if (!collider) continue;

			var inter = collider.GetComponentInParent<IDestructible>();
			if (inter == null) continue;

			set.Add(inter);
		}
		candidates = set.ToList();

		return candidates;
	}
	public void Initialize(IEntityComponentRoot value)
	{
		root = value;
		root.TryGetCapability(out controllerAI);
		hits = new Collider[maxHits];
		parent = root.GameObject.transform;
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying) return;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + agent.height * 0.5f, transform.position.z), radius);
	}
}
