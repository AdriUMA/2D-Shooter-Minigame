using System.Collections;
using UnityEngine;

public class ZoneSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private Zone _zonePrefab;
    [SerializeField] private float _startDelay = 1f;
    [SerializeField] private float _maxRandomnessTime = 7.5f;
    private float _startTime;

    private Coroutine _spawnCoroutine;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_startDelay);
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
            SpawnZone();
            yield return new WaitForSeconds(Random.Range(0.25f, _maxRandomnessTime));
        }
    }

    private void SpawnZone()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Instantiate(_zonePrefab, spawnPoint.position, Quaternion.identity);
    }
}
