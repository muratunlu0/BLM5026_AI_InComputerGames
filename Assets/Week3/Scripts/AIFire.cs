using UnityEngine;

public class AIFire : MonoBehaviour
{
    public GameObject bullet;
    public Transform turret;
    public Transform gun;
    public GameObject target;
    public float speed = 15.0f;
    public float fireDelay = 2.0f;
    public bool useHighAngle = true;

    private float delay;

    public static float? LaunchAngle(Vector3 from, Vector3 to, float speed, bool high)
    {
        Vector3 targetDir = to - from;
        float y = targetDir.y;
        targetDir.y = 0.0f;
        float x = targetDir.magnitude;
        float gravity = 9.8f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot < 0.0f)
            return null;

        float root = Mathf.Sqrt(underTheSqrRoot);
        float tangent = high ? sSqr + root : sSqr - root;
        return Mathf.Atan2(tangent, gravity * x) * Mathf.Rad2Deg;
    }

    public static bool AimTurret(Transform turret, Vector3 gunPosition, Vector3 targetPosition, float speed, bool high)
    {
        Vector3 direction = targetPosition - turret.position;
        direction.y = 0.0f;
        if (direction == Vector3.zero)
            return false;

        float? angle = LaunchAngle(gunPosition, targetPosition, speed, high);
        if (angle == null)
            return false;

        turret.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-(float)angle, 0.0f, 0.0f);
        return true;
    }

    public static void Launch(GameObject bullet, Transform gun, float speed)
    {
        GameObject shell = Instantiate(bullet, gun.position, gun.rotation);
        shell.GetComponent<Rigidbody>().linearVelocity = speed * gun.forward;
    }

    void Update()
    {
        delay -= Time.deltaTime;

        if (!AimTurret(turret, gun.position, target.transform.position, speed, useHighAngle))
            return;

        if (delay <= 0.0f)
        {
            Launch(bullet, gun, speed);
            delay = fireDelay;
        }
    }
}
