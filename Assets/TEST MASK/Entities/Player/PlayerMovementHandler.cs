using System;
using DG.Tweening;
using UnityEngine;

public class PlayerMovementHandler : MovementHandler
{
    private float playerSpeed = 3f;
    public override Vector2 CurrentMovementDirection => getDirection();
    public Vector2 LastMovementDirection { get; private set; }

    private Func<Vector2> getDirection;
    public PlayerMovementHandler(Transform playerTransform, float defaultSpeed, Func<Vector2> directionGetter) : base(playerTransform)
    {
        getDirection = directionGetter;
        playerSpeed = defaultSpeed;
    }

    public void UpdatePlayerMovement()
    {
        Vector3 direction = new Vector3(CurrentMovementDirection.x, CurrentMovementDirection.y, 0);
        entityTransform.position += direction * playerSpeed * Time.deltaTime;
        SaveLastMovementDirection();
    }

    public void SaveLastMovementDirection()
    {
        if (CurrentMovementDirection == Vector2.zero) return;
        LastMovementDirection = CurrentMovementDirection;
        Debug.Log(LastMovementDirection);
    }
}
