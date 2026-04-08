using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBase : MonoBehaviour
{
	public abstract IList<IDestructible> Candidates { get; }
}
