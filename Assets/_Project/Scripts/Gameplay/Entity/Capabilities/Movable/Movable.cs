using UnityEngine;
using UnityEngine.AI;

public class Movable : MonoBehaviour, IMovable
{
	public IEntityComponentRoot Root => root;

	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private float movementSpeed;

	private NavMeshPath path;
	private IEntityComponentRoot root;

	private void Awake()
	{
		path = new();
	}

	public void Initialize(IEntityComponentRoot value)
	{
		root = value;
		agent.speed = root.Config.MovementSpeed;
	}
	private void OnEnable()
	{
		NavMeshHit hit;

		if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
			agent.Warp(hit.position);
	}

    public void StartMoving(Transform target)
	{
		SetDestination(target);
		agent.isStopped = false;
	}
	public void StopMoving()
	{
		agent.isStopped = true; // Do in controller
	}
	public void SetDestination(Transform target)
	{
		agent.CalculatePath(target.position, path);
		agent.SetPath(path);
		agent.isStopped = false;
	}
	public void SetPosition(Vector3 position) => agent.Warp(position);
}
