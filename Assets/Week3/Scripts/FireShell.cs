using UnityEngine;
using UnityEngine.InputSystem;

public class FireShell : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;
    public Transform gun;
    public GameObject ballisticBullet;
    public float ballisticSpeed = 15.0f;
    public bool useHighAngle = true;
    public float fireDelay = 0.2f;

    private float delay;

    void CreateBullet()
    {
        Instantiate(bullet, turret.transform.position, turret.transform.rotation);
    }

    Vector3 CalculateTrajectory()
    {
        Vector3 p = enemy.transform.position - transform.position;
        Vector3 v = enemy.transform.forward * enemy.GetComponent<Drive>().speed;
        float s = bullet.GetComponent<MoveShell>().speed;

        float a = Vector3.Dot(v, v) - s * s;
        float b = Vector3.Dot(p, v);
        float c = Vector3.Dot(p, p);
        float d = b * b - a * c;

        if (d < 0.1f)
            return Vector3.zero;

        float sqrt = Mathf.Sqrt(d);
        float t1 = (-b - sqrt) / c;
        float t2 = (-b + sqrt) / c;

        float t;
        if (t1 < 0.0f && t2 < 0.0f)
            return Vector3.zero;
        else if (t1 < 0.0f)
            t = t2;
        else if (t2 < 0.0f)
            t = t1;
        else
            t = Mathf.Max(t1, t2);

        return t * p + v;
    }

    float FlightTime(Vector3 from, Vector3 to, float angle)
    {
        Vector3 flat = to - from;
        flat.y = 0.0f;
        return flat.magnitude / (ballisticSpeed * Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    bool BallisticReady()
    {
        return turretBase != null && gun != null && ballisticBullet != null;
    }

    void FireBallistic()
    {
        Vector3 aimPoint = enemy.transform.position;
        Vector3 v = enemy.transform.forward * enemy.GetComponent<Drive>().speed;

        for (int i = 0; i < 3; i++)
        {
            float? angle = AIFire.LaunchAngle(gun.position, aimPoint, ballisticSpeed, useHighAngle);
            if (angle == null)
                return;

            aimPoint = enemy.transform.position + v * FlightTime(gun.position, aimPoint, (float)angle);
        }

        if (!AIFire.AimTurret(turretBase, gun.position, aimPoint, ballisticSpeed, useHighAngle))
            return;

        AIFire.Launch(ballisticBullet, gun, ballisticSpeed);
        delay = fireDelay;
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        delay -= Time.deltaTime;

        if (Keyboard.current.fKey.isPressed && delay <= 0.0f && BallisticReady())
            FireBallistic();

        if (!Keyboard.current.spaceKey.wasPressedThisFrame)
            return;

        Vector3 aimAt = CalculateTrajectory();
        if (aimAt != Vector3.zero)
        {
            transform.forward = aimAt;
            CreateBullet();
        }
    }
}
