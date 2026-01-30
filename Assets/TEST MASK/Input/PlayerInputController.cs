using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController
{
    public InputAction Move { get; private set; }
    public InputAction Hability { get; private set; }

    private BasicPlayerMovement inputActions;
    public Vector2 MovementDirection { get; private set; }

    public PlayerInputController()
    {
        inputActions = new BasicPlayerMovement();

        Move = inputActions.PlayerMovement.Move;
        Hability = inputActions.PlayerMovement.Hability;

        Move.performed += context => MovementDirection = context.ReadValue<Vector2>();
        Move.canceled += context => MovementDirection = context.ReadValue<Vector2>();

        EnableInput();
    }

    public void EnableInput()
    {
        inputActions.Enable();
    }

    public void DisableInput()
    {
        inputActions.Disable();
    }
}
