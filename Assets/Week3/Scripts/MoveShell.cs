using UnityEngine;

public class MoveShell : MonoBehaviour
{
    public float speed = 1.0f;
    public float verticalFactor = 0.0f;

    void Update()
    {
        transform.Translate(0, verticalFactor * speed * Time.deltaTime, speed * Time.deltaTime);
    }
}
