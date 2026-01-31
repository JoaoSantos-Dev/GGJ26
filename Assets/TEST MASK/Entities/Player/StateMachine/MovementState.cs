
public class MovementState : PlayerState
{
    private readonly PlayerController playerController;
    private AnimationController animationController;
    public MovementState(PlayerStateMachine stateMachine, PlayerController playerController) : base(stateMachine)
    {
        this.playerController = playerController;
        this.animationController = this.playerController.AnimationController;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        animationController.StartMoveAnimation();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        animationController.StopMoveAnimation();
    }

    public override void Update()
    {
        UpdateCharacterRendererFlip();
        stateMachine.PlayerMovementHandler.UpdatePlayerMovement();
        stateMachine.TryIdleState();
        stateMachine.TryHabilityState();
        base.Update();
    }
    private void UpdateCharacterRendererFlip()
    {
        var moveDirectionX = stateMachine.PlayerMovementHandler.CurrentMovementDirection.x;
        if (moveDirectionX == 0) return;
        animationController.SpriteRenderer.flipX = moveDirectionX > 0;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
