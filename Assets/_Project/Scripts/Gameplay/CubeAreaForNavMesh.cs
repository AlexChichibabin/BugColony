using UnityEngine;
using UnityEngine.AI;

public class CubeAreaForNavMesh : CubeArea, ICubeArea
{
    public override Vector3 GetRandomInsideZone()
    {
        Vector3 point = GetRandomPoint();

        NavMeshHit hit;

        if (NavMesh.SamplePosition(point, out hit, 5f, NavMesh.AllAreas))
        {
            point = hit.position;
        }
        return point;
    }
}
