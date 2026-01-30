using System;
using UnityEngine;

public class PlayerMovementHandler : MovementHandler
{
    private readonly Func<Vector2> getDirection;
    private readonly float playerSpeed = 3f;

    public PlayerMovementHandler(Transform playerTransform, float defaultSpeed, Func<Vector2> directionGetter) : base(
        playerTransform)
    {
        getDirection = directionGetter;
        playerSpeed = defaultSpeed;
    }

    public override Vector2 CurrentMovementDirection => getDirection();
    public Vector2 LastMovementDirection { get; private set; }

    public void UpdatePlayerMovement()
    {
        var direction = new Vector3(CurrentMovementDirection.x, CurrentMovementDirection.y, 0);
        entityTransform.position += direction * playerSpeed * Time.deltaTime;
        SaveLastMovementDirection();
    }

    public void SaveLastMovementDirection()
    {
        if (CurrentMovementDirection == Vector2.zero) return;
        LastMovementDirection = CurrentMovementDirection;
    }
}