using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public int MuteMode
    {
        get
        {
            var mutemode = PlayerPrefs.GetInt("MuteMode", 0);

            return mutemode % 3;
        }

        private set
        {
            PlayerPrefs.SetInt("MuteMode", value % 3);
        }
    }

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
    public bool IsFXMuted => _fx[0].mute;
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
    public bool IsMusicMuted => _music.mute;
    [SerializeField] private AudioSource _music;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        FXVolume = _fxVolume;
        MusicVolume = _musicVolume;

        RefreshAudioSources();
    }

    /// <summary>
    /// Play sound effect with random pitch
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySFX(AudioClip clip)
    {
        var fx = _fx[_currentFX];
        _currentFX = (_currentFX + 1) % _fx.Length;

        fx.clip = clip;
        fx.pitch = Random.Range(0.9f, 1.1f);
        fx.Play();
    }

    private void SetMusicMute(bool mute)
    {
        _music.mute = mute;
    }

    private void SetFXMute(bool mute)
    {
        foreach (var fx in _fx) fx.mute = mute;
    }

    public void ChangeMuteMode()
    {
        MuteMode++;
        RefreshAudioSources();
    }

    public void RefreshAudioSources()
    {
        switch (MuteMode)
        {
            case 0:
                SetFXMute(false);
                SetMusicMute(false);
                break;
            case 1:
                SetFXMute(false);
                SetMusicMute(true);
                break;
            case 2:
                SetFXMute(true);
                SetMusicMute(true);
                break;
        }
    }
}
