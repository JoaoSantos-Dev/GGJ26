using System;

public class HabilityState : PlayerState
{
    private MaskBase mask;
    public HabilityState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnStateEnter()
    {
        mask = stateMachine.PlayerInventoryController.EquipedMask;
        mask.OnHabilityEnd += stateMachine.ReturnToPreviousState;
        mask.UseMaskHability();
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        mask.OnHabilityEnd -= stateMachine.ReturnToPreviousState;
        mask = null;
        base.OnStateExit();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
