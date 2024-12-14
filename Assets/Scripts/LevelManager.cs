using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private InputActionAsset _inputActions;

    private InputAction _restartAction;
    private InputAction _exitAction;

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
}
