using UnityEngine;

public class FXController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    private ParticleSystem[] _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponents<ParticleSystem>();
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
            var clip = _clips[Random.Range(0, _clips.Length)];
            AudioManager.Instance.PlaySFX(clip);
        }

        if (lifeTime > 0f) lifeTime += 0.1f;
        Destroy(gameObject, lifeTime);
    }
}
