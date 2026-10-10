using UnityEngine;

public class DestroyShell : MonoBehaviour
{
    public float lifeTime = 3.0f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
