using System.Collections.Generic;
using System.Linq;
using Zenject;

public class TrackerTrigger : TriggerBase, ITrigger
{
	public override IList<IDestructible> Candidates => GetTargetCandidates();
	public IEntityComponentRoot Root => root;


	private IEntityComponentRoot root;

	[Inject] private IEntityTracker tracker;

	private List<IDestructible> GetTargetCandidates()
	{
		return tracker.AllActiveEntities.ToList();
	}

	public void Initialize(IEntityComponentRoot value)
	{
		root = value;
	}

	public void OnSpawned()
	{
		
	}

	public void OnDespawned()
	{
		
	}
}
