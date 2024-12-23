using UnityEngine;
using UnityEngine.Events;

public class Player : MonoSingleton<Player>
{
    private PlayerInput _inputs;

    public UnityEvent OnDeath;

    public bool IsDead { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
        _inputs = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _inputs.Move.Enable();
        _inputs.Attack.Enable();
        _inputs.SecondaryAttack.Enable();
        _inputs.Mouse.Enable();
    }

    private void OnDisable()
    {
        _inputs.Move.Disable();
        _inputs.Attack.Disable();
        _inputs.SecondaryAttack.Disable();
        _inputs.Mouse.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (!enemy.Running) return;

            Debug.Log("Player died");

            IsDead = true;
            OnDeath.Invoke();

            _inputs.Move.Disable();
            _inputs.Attack.Disable();
            _inputs.SecondaryAttack.Disable();
            _inputs.Mouse.Disable();
        }
    }
}
