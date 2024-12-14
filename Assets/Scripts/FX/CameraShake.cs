using System.Collections;
using UnityEngine;

public class CameraShake : MonoSingleton<CameraShake>
{
    [SerializeField] private float _shakeMaxDuration = 0.4f;
    private float _currentShakeDuration = 0f;
    private float _currentShakeAmount = 0f;
    [SerializeField] private float _extraRandomShake = 0.02f;
    [SerializeField] private float _maxShakeAccumulated = 0.5f;
    [SerializeField] private float _decreaseFactor = 1.0f;
    [SerializeField] private float _scaleMultiplier = 0.25f;

    private float _minimumShake = 0f;

    private Vector3 _originalPosition;

    public float CurrentShakeAmount => _currentShakeAmount;

    protected override void Awake()
    {
        _originalPosition = transform.position;
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(ShakeCoroutine());
    }

    public void SetMinShake(float minimumShake)
    {
        _minimumShake = minimumShake;
    }

    public void Shake(float addCustomAmount, float addCustomDuration)
    {
        _currentShakeAmount = Mathf.Min(_currentShakeAmount + addCustomAmount + Random.Range(0f, _extraRandomShake), _maxShakeAccumulated);
        _currentShakeDuration = Mathf.Min(addCustomDuration + _currentShakeDuration, _shakeMaxDuration);
    }

    private IEnumerator ShakeCoroutine()
    {
        while (true)
        {
            float shake = Mathf.Max(_currentShakeAmount, _minimumShake) * _scaleMultiplier;

            if (_minimumShake > 0 || _currentShakeDuration > 0)
            {
                transform.position = _originalPosition + Random.insideUnitSphere * shake;
                _currentShakeDuration -= Time.deltaTime * _decreaseFactor;
            }
            else
            {
                transform.position = _originalPosition;
                _currentShakeAmount = 0f;
                _currentShakeDuration = 0f;
            }

            yield return null;
        }
        
    }
}
