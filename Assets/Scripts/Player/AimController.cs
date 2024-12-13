using UnityEngine;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour
{
    private PlayerInput _inputs;
    private InputAction _mouse;

    private Vector2 _mousePosition;

    private void Awake()
    {
        _inputs = GetComponent<PlayerInput>();
        _mouse = _inputs.Mouse;
    }

    private void OnEnable()
    {
        _mouse.performed += MouseInput;
    }

    private void OnDisable()
    {
        _mouse.performed -= MouseInput;
    }

    private void MouseInput(InputAction.CallbackContext ctx)
    {
        _mousePosition = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(_mousePosition);
        mouseWorldPosition.z = 0;
        transform.up = mouseWorldPosition - transform.position;
    }
}
