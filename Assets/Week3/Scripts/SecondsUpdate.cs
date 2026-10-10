using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    public float speed = 1.0f;

    private float startOffset;
    private float startZ;
    private bool gotStartTime;

    void Update()
    {
        if (!gotStartTime)
        {
            startOffset = Time.realtimeSinceStartup;
            startZ = transform.position.z;
            gotStartTime = true;
        }

        float z = startZ + (Time.realtimeSinceStartup - startOffset) * speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
}
