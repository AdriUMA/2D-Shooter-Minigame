using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public float speed = 1f;
    public float health = 100f;

    [Header("Spawning")]
    [SerializeField] private float _spawnDelay = 1f;

    [Header("Taking damage")]
    [SerializeField] private AudioClip _hitSound;

    [Header("OnDeath")]
    [SerializeField] private bool _shakeCamera = true;
    [SerializeField] private float _shakeAmount = 0.01f;
    [SerializeField] private float _shakeDuration = 0.05f;
    [SerializeField] private AudioClip _deathSound;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    protected NavMeshAgent agent;

    private Coroutine _agentDestinationUpdated;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        agent.speed = speed;
    }

    private void OnEnable()
    {
        Player.Instance.OnDeath.AddListener(OnPlayerDeath);
    }

    private void OnDisable()
    {
        Player.Instance.OnDeath.RemoveListener(OnPlayerDeath);
    }

    protected virtual IEnumerator Start()
    {
        float instancedTime = Time.time;
        Color color = _spriteRenderer.color;
        _collider.enabled = false;

        while (Time.time - instancedTime < _spawnDelay)
        {
            color.a = Mathf.Lerp(0, 1, (Time.time - instancedTime) / _spawnDelay);
            _spriteRenderer.color = color;
            yield return null;
        }

        _collider.enabled = true;
        _spriteRenderer.color = color;

        Run();
    }

    public void Run()
    {
        if (Player.Instance.IsDead) return;
        if (_agentDestinationUpdated != null) return;

        _agentDestinationUpdated = StartCoroutine(AgentDestinationUpdated());
    }

    public void Stop()
    {
        if (_agentDestinationUpdated == null) return;

        StopCoroutine(_agentDestinationUpdated);
        _agentDestinationUpdated = null;

        agent.ResetPath();
    }

    IEnumerator AgentDestinationUpdated()
    {
        while (true)
        {
            agent.SetDestination(Player.Instance.transform.position);
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void OnPlayerDeath()
    {
        Stop();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        AudioManager.Instance.PlayFX(_hitSound);
        if (health <= 0) OnDeath();
    }

    public void OnDeath()
    {
        AudioManager.Instance.PlayFX(_deathSound);
        CameraShake.Instance.Shake(_shakeAmount, _shakeDuration);
        Destroy(gameObject);
    }
}
