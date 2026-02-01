using System;
using UnityEngine;

public class DashMask : MaskBase
{
    [SerializeField] private float dashDistance = 1f;
    [SerializeField] private float dashSpeed = 0.1f;
    public override event Action OnHabilityEnd;

    protected override void Awake()
    {
        base.Awake();
        MaskType = MaskType.Dash;
    }
    public override void UseMaskHability()
    {
        base.UseMaskHability();

        Vector3 dashDirection = new Vector3(playerMovementHandler.LastMovementDirection.x, playerMovementHandler.LastMovementDirection.y, 0).normalized;
        playerMovementHandler.AddToEntityPosition(dashDirection * dashDistance, dashSpeed, default, default, () => { OnHabilityEnd?.Invoke(); });
    }
}
