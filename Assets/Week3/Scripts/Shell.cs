using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    public float mass = 2.0f;
    public float force = 30.0f;
    public float drag = 0.1f;
    public float gravity = -9.8f;
    public bool continuousForce = false;

    private float acceleration;
    private float speed;
    private float ySpeed;

    void Start()
    {
        acceleration = force / mass;
        speed = acceleration * 1.0f;
    }

    void LateUpdate()
    {
        if (continuousForce)
            speed += acceleration * Time.deltaTime;

        speed *= (1.0f - Time.deltaTime * drag);
        ySpeed += gravity * Time.deltaTime;
        transform.Translate(0, ySpeed * Time.deltaTime, speed * Time.deltaTime);

        if (transform.position.y < 0.0f)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("tank") && explosion != null)
        {
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
        }
        Destroy(gameObject);
    }
}
