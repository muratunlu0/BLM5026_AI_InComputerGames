using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PumpkinController : MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);

        this.transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector3>();
    }
}
