using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxVel = 10f;
    [SerializeField] private float _timeToStop = 0.33f;
    [SerializeField] private AnimationCurve _stopCurve;
    private float _lastMovingTime;

    private Rigidbody2D _rb;

    private PlayerInput _inputs;

    private Vector2 _axisPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inputs = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _inputs.Move.performed += MoveInput;
        _inputs.Move.canceled += MoveInput;
    }

    private void OnDisable()
    {
        _inputs.Move.performed -= MoveInput;
        _inputs.Move.canceled -= MoveInput;
    }

    private void Update()
    {
        if (_axisPosition != Vector2.zero)
        {
            Moving();
        }
        else
        {
            Stopping();
        }
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    private void MoveInput(InputAction.CallbackContext ctx)
    {
        _axisPosition = ctx.ReadValue<Vector2>();
    }

    private void Moving()
    {
        _lastMovingTime = Time.time;
        var forze = _axisPosition * (Time.deltaTime * _speed);
        var velocity = Vector2.ClampMagnitude(_rb.linearVelocity + forze, _maxVel);
        
        _rb.linearVelocity = velocity;
    }

    private void Stopping()
    {
        var t = (Time.time - _lastMovingTime) / _timeToStop;
        var curveValue = _stopCurve.Evaluate(t);
        var velocity = Vector2.Lerp(_rb.linearVelocity, Vector2.zero, curveValue);

        _rb.linearVelocity = velocity;
    }
}
