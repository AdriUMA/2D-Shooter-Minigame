using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoSingleton<LevelManager>
{
    [Header("Points")]
    [SerializeField] private int _pointsPerZone = 50;
    private int _conqueredZones = 0;
    public int ConqueredZones
    {
        get => _conqueredZones;
        set
        {
            _conqueredZones = value;
            UpdateUI();
        }
    }
    [SerializeField] private int _pointsPerNormalEnemy = 10;
    private int _normalEnemiesKilled = 0;
    public int NormalEnemiesKilled
    {
        get => _normalEnemiesKilled;
        set
        {
            _normalEnemiesKilled = value;
            UpdateUI();
        }
    }

    [SerializeField] private int _pointsPerBigEnemy = 20;
    private int _bigEnemiesKilled = 0;
    public int BigEnemiesKilled
    {
        get => _bigEnemiesKilled;
        set
        {
            _bigEnemiesKilled = value;
            UpdateUI();
        }
    }

    [Header("Input")]
    [SerializeField] private InputActionAsset _inputActions;

    private InputAction _restartAction;
    private InputAction _exitAction;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _conqueredZonesText;
    [SerializeField] private TextMeshProUGUI _normalEnemiesKilledText;
    [SerializeField] private TextMeshProUGUI _bigEnemiesKilledText;

    protected override void Awake()
    {
        _restartAction = _inputActions.FindAction("Player/Restart");
        _exitAction = _inputActions.FindAction("Player/Exit");
        base.Awake();
    }

    public void OnEnable()
    {
        _restartAction.Enable();
        _exitAction.Enable();
        _restartAction.performed += Restart;
        _exitAction.performed += Exit;
    }

    public void OnDisable()
    {
        _restartAction.Disable();
        _exitAction.Disable();
        _restartAction.performed -= Restart;
        _exitAction.performed -= Exit;
    }

    public void Restart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(gameObject.scene.buildIndex);
    }

    public void Exit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    private void UpdateUI()
    {
        _scoreText.text = $"Score {ConqueredZones * _pointsPerZone + NormalEnemiesKilled * _pointsPerNormalEnemy + BigEnemiesKilled * _pointsPerBigEnemy}";
        _conqueredZonesText.text = $"{ConqueredZones}";
        _normalEnemiesKilledText.text = $"{NormalEnemiesKilled}";
        _bigEnemiesKilledText.text = $"{BigEnemiesKilled}";
    }
}
