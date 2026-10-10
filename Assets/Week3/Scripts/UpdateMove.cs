using UnityEngine;

public class UpdateMove : MonoBehaviour
{
    public float speed = 1.0f;
    public bool useDeltaTime = true;

    void Update()
    {
        if (useDeltaTime)
            transform.Translate(0, 0, speed * Time.deltaTime);
        else
            transform.Translate(0, 0, speed * 0.02f);
    }
}
