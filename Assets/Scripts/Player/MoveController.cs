using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _initialSpeed = 10f;
    private float _speed = 0;

    private void Awake()
    {
        _speed = _initialSpeed;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    private void MovePlayer(InputAction.CallbackContext ctx)
    {
        var movement = ctx.ReadValue<Vector2>();
        movement *= _speed * Time.deltaTime;

        transform.position += new Vector3(movement.x, movement.y, 0);
    }
}
