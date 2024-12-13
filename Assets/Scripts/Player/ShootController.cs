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

    private Projectile _bulletPrefab;
    private Projectile _bombPrefab;

    private void Awake()
    {
        _bulletPrefab = Resources.Load<Projectile>("Prefabs/Projectiles/Bullet");
        _bombPrefab = Resources.Load<Projectile>("Prefabs/Projectiles/Bomb");
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void InputBullet(InputAction.CallbackContext ctx)
    {
    }

    private void InputBomb(InputAction.CallbackContext ctx)
    {
    }
}
