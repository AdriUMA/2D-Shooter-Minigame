using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public float speed = 1f;
    public float health = 100f;
    protected NavMeshAgent agent;

    private Vector3 PlayerPosition => Player.Instance.transform.position;

    private void Awake()
    {
        if(!TryGetComponent(out agent))
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }

        agent.speed = speed;
    }

    private void Start()
    {
        agent.SetDestination(Vector3.zero);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) OnDeath();
    }

    public abstract void OnDeath();
}
