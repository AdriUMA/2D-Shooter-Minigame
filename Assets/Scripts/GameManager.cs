using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Input")]
    [SerializeField] private InputActionAsset _inputActions;

    private InputAction _muteAction;

    protected override void Awake()
    {
        _muteAction = _inputActions.FindAction("Player/Mute");
        base.Awake();
    }

    public void OnEnable()
    {
        _muteAction.Enable();
        _muteAction.performed += Mute;
    }

    public void OnDisable()
    {
        _muteAction.Disable();
        _muteAction.performed -= Mute;
    }

    public void Mute(InputAction.CallbackContext context)
    {
        if (AudioManager.Instance == null) return;

        AudioManager.Instance.ChangeMuteMode();
    }
}
