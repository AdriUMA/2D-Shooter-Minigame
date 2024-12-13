using UnityEngine;

public class Bomb : Projectile
{
    [SerializeField] private float _areaDamage = 5f;
    [SerializeField] private float _radius = 1f;

    [SerializeField] private FXController _particlePrefab;

    private bool _exploted = false;

    protected override void OnHit(Enemy enemy)
    {
        if(_exploted) return;
        _exploted = true;

        ExplosionFX();

        enemy.TakeDamage(damage);

        var nearColliders = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (var c in nearColliders)
        {
            if (!c.TryGetComponent(out Enemy e)) continue;
            if (e == enemy) continue;

            e.TakeDamage(_areaDamage);
        }
    }

    private void OnDestroy()
    {
        if(_exploted) return;
        _exploted = true;

        ExplosionFX();

        var enemies = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (var enemy in enemies)
        {
            if (enemy.TryGetComponent(out Enemy e))
            {
                e.TakeDamage(_areaDamage);
            }
        }
    }

    private void ExplosionFX()
    {
        var particles = Instantiate(_particlePrefab, transform.position, Quaternion.identity);
        particles.Play();

        CameraShake.Instance.Shake();
    }
}
