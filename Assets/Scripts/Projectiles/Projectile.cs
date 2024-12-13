using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] protected float damage = 1f;

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            OnHit(enemy);
        }

        Destroy(gameObject);
    }

    protected abstract void OnHit(Enemy enemy);
}
