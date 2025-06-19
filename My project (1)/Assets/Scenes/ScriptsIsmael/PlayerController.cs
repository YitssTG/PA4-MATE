using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;

    [Header("Player Movement Properties")]
    [SerializeField] Vector2 movementInput;
    public static event Action<Vector2> OnMoving;
    [SerializeField] Transform reference;

    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y);
        transform.Translate(direction * speed * Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        OnMoving?.Invoke(movementInput);
    }
}
