using Cysharp.Threading.Tasks;
using UnityEngine;

public class DeathState : PlayerState
{
    private readonly PlayerController playerController;
    // private AnimationController animationController => playerController.AnimationController;
    public DeathState(PlayerStateMachine stateMachine, PlayerController playerController) : base(stateMachine)
    {
        this.playerController = playerController;
    }

    public override void OnStateEnter()
    {
        playerController.AnimationController.PlayDeath();
        StartDeathBehaviour();
        playerController.Rigidbody2D.simulated = false;
    }

    public override void OnStateExit()
    {
        playerController.Rigidbody2D.simulated = true;
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        
    }

    private async void StartDeathBehaviour()
    { 
        playerController.HeadRenderer.enabled = false;
        playerController.MaskRenderer.enabled = false;
        await UniTask.Delay(1000);
        playerController.Destroy();
    }
    
    
    
}
