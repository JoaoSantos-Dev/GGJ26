
public class MovementState : PlayerState
{
    public MovementState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void Update()
    {
        stateMachine.PlayerMovementHandler.UpdatePlayerMovement();
        stateMachine.TryIdleState();
        stateMachine.TryHabilityState();
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
