
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
        var movingToRight = moveDirectionX > 0;
        animationController.SpriteRenderer.flipX = movingToRight;
        animationController.MaskRenderer.flipX = movingToRight;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
