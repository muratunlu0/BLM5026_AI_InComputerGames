using UnityEngine;
using UnityEngine.InputSystem;

public class Drive : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 100.0f;
    public bool autoDrive = false;
    public float patrolDistance = 0.0f;
    public Transform turret;
    public float turretSpeed = 60.0f;
    public Transform gun;
    public GameObject bulletObj;

    private Vector2 moveInput;
    private Vector3 patrolStart;

    void Start()
    {
        patrolStart = transform.position;
    }

    void Update()
    {
        if (autoDrive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            if (patrolDistance > 0.0f && Vector3.Distance(patrolStart, transform.position) >= patrolDistance)
            {
                transform.Rotate(0, 180.0f, 0);
                patrolStart = transform.position;
            }
            return;
        }

        transform.Translate(0, 0, moveInput.y * speed * Time.deltaTime);
        transform.Rotate(0, moveInput.x * rotationSpeed * Time.deltaTime, 0);

        if (Keyboard.current == null || turret == null)
            return;

        if (Keyboard.current.tKey.isPressed)
            turret.RotateAround(turret.position, turret.right, -turretSpeed * Time.deltaTime);
        else if (Keyboard.current.gKey.isPressed)
            turret.RotateAround(turret.position, turret.right, turretSpeed * Time.deltaTime);
        else if (Keyboard.current.bKey.wasPressedThisFrame && gun != null && bulletObj != null)
            Instantiate(bulletObj, gun.position, gun.rotation);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
