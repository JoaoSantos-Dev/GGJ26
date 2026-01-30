using System;using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerInputController: IDisposable
{
    public InputAction Move { get; private set; }
    public InputAction Hability { get; private set; }

    // private BasicPlayerMovement inputActions;
    public Vector2 MovementDirection { get; private set; }

    public PlayerInputController(PlayerInput playerInput)
    {
        Move = playerInput.actions["Move"];
         Hability = playerInput.actions["Hability"];
        // inputActions = new BasicPlayerMovement();

        // Move = inputActions.PlayerMovement.Move;
        // Hability = inputActions.PlayerMovement.Hability;

        EnableInput();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
         MovementDirection = context.ReadValue<Vector2>();
    }

    public void EnableInput()
    {
        // inputActions.Enable();
        Move.performed += OnMovePerformed;
        Move.canceled += OnMoveCanceled;
    }

    public void DisableInput()
    {
        // inputActions.Disable();
        Move.performed -= OnMovePerformed;
        Move.canceled -= OnMoveCanceled;
    }

    public void Dispose()
    {
        DisableInput();
    }
}
