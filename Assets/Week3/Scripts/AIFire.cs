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

    void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, gun.position, gun.rotation);
        shell.GetComponent<Rigidbody>().linearVelocity = speed * gun.forward;
    }

    float? CalculateAngle(bool high)
    {
        Vector3 targetDir = target.transform.position - gun.position;
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

    void Update()
    {
        delay -= Time.deltaTime;

        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0.0f;
        if (direction == Vector3.zero)
            return;

        float? angle = CalculateAngle(useHighAngle);
        if (angle == null)
            return;

        turret.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-(float)angle, 0.0f, 0.0f);

        if (delay <= 0.0f)
        {
            CreateBullet();
            delay = fireDelay;
        }
    }
}
