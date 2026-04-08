using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CubeArea : MonoBehaviour, ICubeArea
{
	[SerializeField] protected Vector3 Area;

	public void SetArea(Vector3 area) => Area = area;
	public virtual Vector3 GetRandomInsideZone()
	{
		return GetRandomPoint();
	}
	protected Vector3 GetRandomPoint()
	{
        Vector3 localResult = transform.localPosition;

        localResult.x += Random.Range(-Area.x / 2, Area.x / 2);
        localResult.y += Random.Range(-Area.y / 2, Area.y / 2);
        localResult.z += Random.Range(-Area.z / 2, Area.z / 2);

        Vector3 worldResult = transform.TransformPoint(localResult) - transform.localPosition;

        return worldResult;
    }

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, Area);
	}
#endif
}
