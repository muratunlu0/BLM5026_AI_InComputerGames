using UnityEngine;

public class Moving : MonoBehaviour {

    public GameObject goal;
    private Vector3 defaultPos;
    [SerializeField] private float speed = 4.0f;

    void Start() {

        defaultPos = transform.position;
        //transform.LookAt(goal.transform.position);
    }

    void Update() 
    {
        if (goal == null)
            return;

        Vector3 direction = goal.transform.position - transform.position;
        transform.forward = direction.normalized;

        if (direction.sqrMagnitude > 0.000001f)
            transform.position += direction.normalized * speed * Time.deltaTime;
        else
            transform.position = defaultPos;
    }
}
