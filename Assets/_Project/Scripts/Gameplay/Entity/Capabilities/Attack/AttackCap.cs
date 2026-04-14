using UnityEngine;
public class AttackCap : MonoBehaviour, IAttack
{
	public IEntityComponentRoot Root => root;

	private IDestructible thisDestructible;
	private IEntityComponentRoot root;
	private IFeedable feedable;

	private int attackDamage;

	private void Start()
	{
		
	}

	public IDestructible Attack(IDestructible target)
    {
		if (thisDestructible == null) return null;
		IDestructible targetDiedDest = target.ApplyDamage(attackDamage, thisDestructible);
		if (targetDiedDest == null) return null;
		feedable.AddFood(target.Root.Config.FoodValue);
		return targetDiedDest;
    }

	public void Initialize(IEntityComponentRoot value)
	{
		root = value;
		attackDamage = root.Config.AttackDamage;
        root.TryGetCapability(out feedable);
        root.TryGetCapability(out thisDestructible);
	}
}
