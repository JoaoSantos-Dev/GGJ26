using UnityEngine;

public class PlayerStateFactory
{
    public PlayerState IdleState { get; private set; }
    public PlayerState MovementState { get; private set; }
    public PlayerState HabilityState { get; private set; }
    public DeathState DeathState { get; private set; }  
    public PlayerStateFactory(PlayerStateMachine stateMachine, PlayerController playerController)
    {
        IdleState = new IdleState(stateMachine);
        MovementState = new MovementState(stateMachine, playerController);
        HabilityState = new HabilityState(stateMachine);
        DeathState = new DeathState(stateMachine, playerController);
    }
}
