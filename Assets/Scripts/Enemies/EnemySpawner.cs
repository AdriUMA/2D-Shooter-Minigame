using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _hardSpawnRate = 2.5f;
    [SerializeField] private float _easySpawnRate = 15f;
    [SerializeField] private AnimationCurve _spawnOverTime;
    [SerializeField] private float _curveTime = 160f;
    [SerializeField] private float _startDelay = 3f;
    [SerializeField] private float _randomnessTime = 7.5f;
    private float _startTime;

    private Coroutine _spawnCoroutine;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_startDelay + Random.Range(-_randomnessTime, _randomnessTime));
        Run();
    }

    private void OnEnable()
    {
        Player.Instance.OnDeath.AddListener(OnPlayerDeath);
    }
    private void OnDisable()
    {
        Player.Instance.OnDeath.RemoveListener(OnPlayerDeath);
    }

    public void Run()
    {
        if (Player.Instance.IsDead) return;
        if (_spawnCoroutine != null) return;

        _startTime = Time.time;

        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void Stop()
    {
        if (_spawnCoroutine == null) return;

        StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = null;
    }

    private void OnPlayerDeath()
    {
        Stop();
    }

    IEnumerator SpawnCoroutine()
    {
        _startTime = Time.time;

        while (true)
        {
            SpawnEnemy();
            float spawnRate = Mathf.Lerp(_easySpawnRate, _hardSpawnRate, _spawnOverTime.Evaluate((Time.time - _startTime) / _curveTime));
            float randomness = Random.Range(-_randomnessTime, _randomnessTime);
            yield return new WaitForSeconds(spawnRate + randomness);
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Instantiate(_enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
