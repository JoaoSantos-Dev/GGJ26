
using System.Diagnostics;

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine)
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
        base.Update();
        stateMachine.TryHabilityState();
        stateMachine.TryMovementState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
