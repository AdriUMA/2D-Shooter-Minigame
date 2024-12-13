using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    /// <summary>
    /// Cuando el usuario comienza a disparar
    /// </summary>
    public UnityEvent OnStartShooting;

    /// <summary>
    /// Cuando se lanza un disparo
    /// </summary>
    public UnityEvent<GameObject> OnShot;

    /// <summary>
    /// Cuando el usuario deja de disparar
    /// </summary>
    public UnityEvent OnStopShooting;

    private PlayerInput _inputs;

    private void Awake()
    {
        _inputs = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _inputs.Attack.started += InputBullet;
        _inputs.Attack.canceled += InputBullet;
        _inputs.SecondaryAttack.started += InputBomb;
        _inputs.SecondaryAttack.canceled += InputBomb;
    }

    private void OnDisable()
    {
        _inputs.Attack.started -= InputBullet;
        _inputs.Attack.canceled -= InputBullet;
        _inputs.SecondaryAttack.started -= InputBomb;
        _inputs.SecondaryAttack.canceled -= InputBomb;
    }

    private void InputBullet(InputAction.CallbackContext ctx)
    {
    }

    private void InputBomb(InputAction.CallbackContext ctx)
    {
    }
}
