using UnityEngine;

public class PlayerStateFactory
{
    public PlayerState IdleState { get; private set; }
    public PlayerState MovementState { get; private set; }
    public PlayerState HabilityState { get; private set; }
    public PlayerStateFactory(PlayerStateMachine stateMachine)
    {
        IdleState = new IdleState(stateMachine);
        MovementState = new MovementState(stateMachine);
        HabilityState = new HabilityState(stateMachine);
    }
}
