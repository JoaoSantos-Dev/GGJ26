
public enum PlayerStateType { Idle, Movement, Hability }
public class PlayerStateMachine
{
    private PlayerInputController playerInputController;
    private PlayerStateFactory stateFactory;
    public PlayerInventoryController PlayerInventoryController { get; private set; }
    public PlayerMovementHandler PlayerMovementHandler { get; private set; }

    public PlayerStateMachine(PlayerInputController playerInputController, 
        PlayerInventoryController playerInventoryController, 
        PlayerMovementHandler playerMovementHandler, PlayerController playerController)
    {
        this.playerInputController = playerInputController;
        PlayerInventoryController = playerInventoryController;
        stateFactory = new PlayerStateFactory(this,playerController );
        CurrentState = stateFactory.IdleState;
        PlayerMovementHandler = playerMovementHandler;
    }
    public PlayerState CurrentState { get; private set; }
    public PlayerState PreviousState { get; private set; }

    public void Update()
    {
        CurrentState.Update();
    }

    public void FixedUpdate()
    {
        CurrentState.FixedUpdate();
    }


    public void TryHabilityState()
    {
        if (playerInputController.Hability.WasPressedThisFrame() && PlayerInventoryController.CanUseMaskHability)
        {
            ChangeState(stateFactory.HabilityState);
        }
    }

    public void TryMovementState()
    {
        if (playerInputController.Move.IsPressed()) ChangeState(stateFactory.MovementState);
    }

    public void TryIdleState()
    {
        if (playerInputController.Move.WasReleasedThisFrame() && CurrentState != stateFactory.HabilityState)
        {
            ChangeState(stateFactory.IdleState);
        }
    }

    public void ReturnToPreviousState()
    {
        ChangeState(PreviousState);
    }

    public void SetState(PlayerStateType state)
    {
        switch (state)
        {
            case PlayerStateType.Idle:
                {
                    ChangeState(stateFactory.IdleState);
                    break;
                }
            case PlayerStateType.Hability:
                {
                    ChangeState(stateFactory.IdleState);
                    break;
                }
            case PlayerStateType.Movement:
                {
                    ChangeState(stateFactory.IdleState);
                    break;
                }

            default:
                {
                    ChangeState(stateFactory.IdleState);
                    break;
                }
        }
    }

    private void ChangeState(PlayerState newState)
    {
        if (CurrentState == newState) return;

        CurrentState.OnStateExit();
        PreviousState = CurrentState;
        CurrentState = newState;
        CurrentState.OnStateEnter();
    }
}
