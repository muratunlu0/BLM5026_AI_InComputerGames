using UnityEngine;

public class AlignToVelocity : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.linearVelocity.sqrMagnitude > 0.001f)
            transform.forward = rb.linearVelocity;
    }
}
