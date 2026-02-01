using System;
using UnityEngine;

public class PlayerMovementHandler : MovementHandler
{
    private readonly Func<Vector2> getDirection;
    private readonly float playerSpeed = 3f;
    private readonly Rigidbody2D rigidbody2D;

    public PlayerMovementHandler(Transform playerTransform, Rigidbody2D rigidbody2D, float defaultSpeed, Func<Vector2> directionGetter) : base(
        playerTransform)
    {
        getDirection = directionGetter;
        playerSpeed = defaultSpeed;
        this.rigidbody2D = rigidbody2D;
    }

    public override Vector2 CurrentMovementDirection => getDirection();
    public Vector2 LastMovementDirection { get; private set; }

    public void UpdatePlayerMovement()
    {
        Vector3 direction = CurrentMovementDirection;
        direction.z = 0;
        var finalDirection = direction.normalized * (playerSpeed * Time.fixedDeltaTime);
        rigidbody2D.linearVelocity = finalDirection;
        // EntityTransform.position += 
        SaveLastMovementDirection();
    }

    public void SaveLastMovementDirection()
    {
        if (CurrentMovementDirection == Vector2.zero) return;
        LastMovementDirection = CurrentMovementDirection;
    }
}