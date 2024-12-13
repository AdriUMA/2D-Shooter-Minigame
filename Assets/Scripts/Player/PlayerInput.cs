using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActions;

    public InputAction Move => _inputActions.FindAction("Player/Move");
    public InputAction Attack => _inputActions.FindAction("Player/Attack");
    public InputAction SecondaryAttack => _inputActions.FindAction("Player/SecondaryAttack");
    public InputAction Mouse => _inputActions.FindAction("Player/Mouse");
}
