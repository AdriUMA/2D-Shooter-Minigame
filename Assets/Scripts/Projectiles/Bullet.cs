using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private FXController _particlePrefab;

    protected override void OnHit(Enemy enemy)
    {
        enemy.TakeDamage(damage);

        if(_particlePrefab == null) return;
        var particles = Instantiate(_particlePrefab, transform.position, Quaternion.identity);
        particles.Play();
    }
}
