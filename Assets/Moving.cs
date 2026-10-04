using UnityEngine;

public class Moving : MonoBehaviour
{
    public GameObject goal;
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private float stopDistance = 1.5f;
    [SerializeField] private float fieldOfView = 90f;
    [SerializeField] private float viewDistance = 10f;

    void Update()
    {
        if (goal == null)
            return;

        Vector3 direction = goal.transform.position - transform.position;
        direction.y = 0f;

        if (!CanSee(direction))
            return;

        transform.forward = direction.normalized;

        float distanceSqr = direction.sqrMagnitude;
        float stopDistanceSqr = stopDistance * stopDistance;

        if (distanceSqr > stopDistanceSqr)
            transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private bool CanSee(Vector3 toTarget)
    {
        if (toTarget == Vector3.zero)
            return false;

        if (toTarget.sqrMagnitude > viewDistance * viewDistance)
            return false;

        float angle = Vector3.Angle(transform.forward, toTarget);
        float halfFOV = fieldOfView * 0.5f;

        return angle <= halfFOV;
    }

    private void OnDrawGizmos()
    {
        bool visible = false;

        if (goal != null)
        {
            Vector3 toTarget = goal.transform.position - transform.position;
            toTarget.y = 0f;
            visible = CanSee(toTarget);
        }

        Gizmos.color = visible ? Color.green : Color.red;

        float halfFOV = fieldOfView * 0.5f;
        Vector3 origin = transform.position;
        Vector3 previous = origin;
        int segments = 24;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -halfFOV + fieldOfView * i / segments;
            Vector3 point = origin + Quaternion.Euler(0f, angle, 0f) * transform.forward * viewDistance;
            Gizmos.DrawLine(previous, point);
            previous = point;
        }

        Gizmos.DrawLine(previous, origin);

        if (visible)
            Gizmos.DrawLine(origin, goal.transform.position);
    }
}
