using System.Collections.Generic;

public interface ITargetResolver
{
	IDestructible Resolve(TargetContext ctx);
}