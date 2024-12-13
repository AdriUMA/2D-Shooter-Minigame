using System.Collections;
using UnityEngine;

public class CameraShake : MonoSingleton<CameraShake>
{
    [SerializeField] private float _shakeDuration = 0.1f;
    [SerializeField] private float _shakeMaxDuration = 0.4f;
    private float _currentShakeDuration = 0f;
    [SerializeField] private float _shakeAmount = 0.05f;
    private float _currentShakeAmount = 0f;
    [SerializeField] private float _extraRandomShake = 0.02f;
    [SerializeField] private float _maxShakeAccumulated = 0.5f;
    [SerializeField] private float _decreaseFactor = 1.0f;

    private Coroutine _shakeCoroutine;
    private Vector3 _originalPosition;


    protected override void Awake()
    {
        _originalPosition = transform.position;
        base.Awake();
    }

    public void Shake()
    {
        _currentShakeAmount = Mathf.Min(_currentShakeAmount + _shakeAmount + Random.Range(0f, _extraRandomShake), _maxShakeAccumulated);
        _currentShakeDuration = Mathf.Min(_shakeDuration + _currentShakeDuration, _shakeMaxDuration);


        if (_shakeCoroutine == null) _shakeCoroutine = StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float shakeAmount = _shakeAmount + Random.Range(0f, _extraRandomShake);

        while (_currentShakeDuration > 0)
        {
            transform.position = _originalPosition + Random.insideUnitSphere * _currentShakeDuration;
            _currentShakeDuration -= Time.deltaTime * _decreaseFactor;
            yield return null;
        }

        transform.position = _originalPosition;
        _shakeCoroutine = null;
    }
}
