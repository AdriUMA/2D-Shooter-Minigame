using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [Header("FX")]
    [Range(0, 1), SerializeField] private float _fxVolume = 1;
    public float FXVolume
    {
        get => _fxVolume;
        set
        {
            _musicVolume = Mathf.Clamp01(value);
            foreach (var fx in _fx) fx.volume = _fxVolume;
        }
    }
    [SerializeField] private AudioSource[] _fx;
    private int _currentFX;

    [Header("Music")]
    [Range(0, 1), SerializeField] private float _musicVolume = 1;
    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = Mathf.Clamp01(value);
            _music.volume = _musicVolume;
        }
    }
    [SerializeField] private AudioSource _music;

    private void Start()
    {
        FXVolume = _fxVolume;
        MusicVolume = _musicVolume;
    }

    /// <summary>
    /// Play sound effect with random pitch
    /// </summary>
    /// <param name="clip"></param>
    public void PlayFX(AudioClip clip)
    {
        var fx = _fx[_currentFX];
        _currentFX = (_currentFX + 1) % _fx.Length;

        fx.clip = clip;
        fx.pitch = Random.Range(0.9f, 1.1f);
        fx.Play();
    }
}
