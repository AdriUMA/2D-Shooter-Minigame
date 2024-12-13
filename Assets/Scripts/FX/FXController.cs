using UnityEngine;

public class FXController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    private ParticleSystem[] _particleSystem;
    private AudioSource _audioSource;

    private void Awake()
    {
        _particleSystem = GetComponents<ParticleSystem>();

        if (!TryGetComponent(out _audioSource))
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Play()
    {
        float lifeTime = 0f;

        foreach (var ps in _particleSystem)
        {
            ps.Play();
            lifeTime = Mathf.Max(lifeTime, ps.main.duration);
        }

        if (_clips.Length > 0)
        {
            _audioSource.clip = _clips[Random.Range(0, _clips.Length)];
            lifeTime = Mathf.Max(lifeTime, _audioSource.clip.length);
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.Play();
        }

        if (lifeTime > 0f) lifeTime += 0.1f;

        Destroy(gameObject, lifeTime);
    }
}
