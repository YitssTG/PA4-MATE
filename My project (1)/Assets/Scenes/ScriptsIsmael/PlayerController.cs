using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Properties")]
    [SerializeField] private float speed;
    [SerializeField] Vector2 movementInput;

    [Header("Jump Properties")]
    [SerializeField] private float jumoForce;
    public bool canJump;

    [Header("Raycast Properties")]
    [SerializeField] private Transform _origin;
    [SerializeField] private Vector3 _direction = Vector3.down;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerInteraction;

    [Header("DrawRay Properties")]
    [SerializeField] private Color OnCollisionRay = Color.green;
    [SerializeField] private Color OnNotCollisionRay = Color.white;

    private Rigidbody _rigidbody;
    private bool isMoving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y);
        _rigidbody.linearVelocity = new Vector3(direction.x * speed, _rigidbody.linearVelocity.y, direction.z * speed);

        isMoving = direction.magnitude > 0.1f;
        RaycastHit hit;
        if (Physics.Raycast(_origin.position, _direction, out hit, _distance, _layerInteraction))
        {
            Debug.DrawRay(_origin.position, _direction * _distance, OnCollisionRay);
            canJump = true;
        }
        else
        {
            Debug.DrawRay(_origin.position, _direction * _distance, OnNotCollisionRay);
            canJump = false;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            _rigidbody.AddForce(Vector3.up * jumoForce, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            Debug.Log("Win");
        }
    }
}
