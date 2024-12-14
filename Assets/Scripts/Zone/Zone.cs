using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private float _timeToFill = 1f;
    [SerializeField] private SpriteRenderer _fillSpriteRenderer;
    [SerializeField] private AudioClip _conqueredSFX;
    [SerializeField] private float _conqueringShakeAmount = 0.01f;
    [SerializeField] private float _conqueredShakeAmount = 0.15f;
    [SerializeField] private float _conqueredShakeTime = 0.1f;

    private float _progression = 0;

    private bool _isPlayerInZone;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isPlayerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isPlayerInZone = false;
        }
    }

    private void Update()
    {
        if (_isPlayerInZone)
        {
            _progression += Time.deltaTime / _timeToFill;
            CameraShake.Instance.Shake(_conqueredShakeAmount * Time.deltaTime * _progression, Time.deltaTime);
            if (_progression >= 1) OnFilled();
        }
        else
        {
            _progression -= Time.deltaTime / _timeToFill;
        }

        _progression = Mathf.Clamp01(_progression);

        _fillSpriteRenderer.transform.localScale = Vector3.one * _progression;
    }

    private void OnFilled()
    {
        AudioManager.Instance.PlaySFX(_conqueredSFX);
        LevelManager.Instance.conqueredZones++;
        CameraShake.Instance.Shake(_conqueredShakeAmount, _conqueredShakeTime);
        Destroy(gameObject);
    }
}
