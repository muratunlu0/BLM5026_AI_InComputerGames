using UnityEngine;

public class DrawVectrors : MonoBehaviour
{
    [SerializeField] Transform p;
    [SerializeField] Transform q;
    [SerializeField] Vector3 v = new Vector3(4, -1, 0);
    [SerializeField] private Transform targetObject;

    private void OnDrawGizmos()
    {
        if (p != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(p.position, p.position + v);
        }

        if (q != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(q.position, q.position + v);
        }
    }
}
