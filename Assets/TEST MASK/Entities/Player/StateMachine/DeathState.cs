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
        StartDeathBehaviour();
    }

    public override void OnStateExit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        
    }

    private async void StartDeathBehaviour()
    {
       await UniTask.Delay(1000);
       playerController.Destroy();
    }
    
    
    
}
