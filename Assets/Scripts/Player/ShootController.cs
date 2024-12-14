using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [Serializable]
    private class Weapon
    {
        public Projectile projectile;
        public float cooldown;
        [HideInInspector] public float lastShot;
    }

    [SerializeField] private Weapon _mainWeapon;
    [SerializeField] private Weapon _secondaryWeapon;
    private readonly List<Weapon> _weaponsPerformed = new List<Weapon>();
    private Weapon CurrentWeapon => _weaponsPerformed.Count == 0 ? null : _weaponsPerformed[^1];
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private AudioClip[] _shootSFX;

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

    private void Update()
    {
        if (CurrentWeapon == null) return;

        if (Time.time - CurrentWeapon.lastShot > CurrentWeapon.cooldown)
        {
            CurrentWeapon.lastShot = Time.time;
            var projectile = Instantiate(CurrentWeapon.projectile, _shootPoint.position, _shootPoint.rotation);
            AudioManager.Instance.PlayFX(_shootSFX[UnityEngine.Random.Range(0, _shootSFX.Length)]);
            OnShot?.Invoke(projectile.gameObject);
        }
    }

    private void OnEnable()
    {
        _inputs.Attack.started += HandlePrimaryAttack;
        _inputs.Attack.canceled += HandlePrimaryAttack;
        _inputs.SecondaryAttack.started += HandleSecondaryAttack;
        _inputs.SecondaryAttack.canceled += HandleSecondaryAttack;
    }

    private void OnDisable()
    {
        _inputs.Attack.started -= HandlePrimaryAttack;
        _inputs.Attack.canceled -= HandlePrimaryAttack;
        _inputs.SecondaryAttack.started -= HandleSecondaryAttack;
        _inputs.SecondaryAttack.canceled -= HandleSecondaryAttack;
    }

    private void HandlePrimaryAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            OnStartShooting?.Invoke();
            _weaponsPerformed.Add(_mainWeapon);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            OnStopShooting?.Invoke();
            _weaponsPerformed.Remove(_mainWeapon);
        }
    }

    private void HandleSecondaryAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            OnStartShooting?.Invoke();
            _weaponsPerformed.Add(_secondaryWeapon);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            OnStopShooting?.Invoke();
            _weaponsPerformed.Remove(_secondaryWeapon);
        }
    }
}
