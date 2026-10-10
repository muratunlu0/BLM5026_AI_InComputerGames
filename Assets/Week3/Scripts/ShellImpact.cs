using UnityEngine;

public class ShellImpact : MonoBehaviour
{
    public GameObject explosion;

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
