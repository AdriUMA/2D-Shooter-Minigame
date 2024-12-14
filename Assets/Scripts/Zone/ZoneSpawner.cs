using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private Zone _zonePrefab;
    [SerializeField] private int _maxZones = 2;
    [SerializeField] private float _startDelay = 1f;
    [SerializeField] private float _maxRandomnessTime = 7.5f;

    private Coroutine _spawnCoroutine;

    private readonly List<Zone> _zones = new List<Zone>();

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
        while (true)
        {
            SpawnZone();
            yield return new WaitForSeconds(Random.Range(0.25f, _maxRandomnessTime));

            while(_zones.Count >= _maxZones)
            {
                yield return new WaitForSeconds(Random.Range(0.25f, 1f));
                _zones.RemoveAll(zone => zone == null);
            }
        }
    }

    private void SpawnZone()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        _zones.Add(Instantiate(_zonePrefab, spawnPoint.position, Quaternion.identity));
    }
}
