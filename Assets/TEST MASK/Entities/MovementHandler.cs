using System;
using DG.Tweening;
using UnityEngine;

public class MovementHandler
{
    public virtual Vector2 CurrentMovementDirection { get; }

    // Change the movement logic to a Rigidbody 2D, if desirable.
    public Transform EntityTransform { get; private set; }

    public MovementHandler(Transform entityTransform)
    {
        this.EntityTransform = entityTransform;
    }

    public void SetEntityPosition(Vector3 newPosition)
    {
        EntityTransform.position = newPosition;
    }
    public void SetEntityPosition(Vector3 newPosition, float duration = 0f, Ease easeType = Ease.Linear, float delay = 0f, Action onComplete = null)
    {
        EntityTransform.DOMove(newPosition, duration).SetEase(easeType).SetDelay(delay).OnComplete(() => onComplete?.Invoke());
    }
    public void AddToEntityPosition(Vector3 positionToAdd)
    {
        EntityTransform.position += positionToAdd;
    }
    public void AddToEntityPosition(Vector3 positionToAdd, float duration = 0f, Ease easeType = Ease.Linear, float delay = 0f, Action onComplete = null)
    {
        EntityTransform.DOMove(EntityTransform.position + positionToAdd, duration).SetEase(easeType).SetDelay(delay).OnComplete(() => onComplete?.Invoke());
    }

    public void PushEntity(float pushForce, Vector3 direction, float duration = 0f, Ease easeType = Ease.Linear, float delay = 0f, Action onComplete = null)
    {
        Vector3 newPosition = EntityTransform.position + direction * pushForce;
        EntityTransform.DOMove(newPosition, duration).SetEase(easeType).SetDelay(delay).OnComplete(() => onComplete?.Invoke());
    }
    public void PushEntityBackwards(float pushForce, float duration = 0f, Ease easeType = Ease.Linear, float delay = 0f, Action onComplete = null)
    {
        Vector3 direction = new Vector3 (-CurrentMovementDirection.x, -CurrentMovementDirection.y, 0).normalized;
        Vector3 newPosition = EntityTransform.position + direction * pushForce;
        EntityTransform.DOMove(newPosition, duration).SetEase(easeType).SetDelay(delay).OnComplete(() => onComplete?.Invoke());
    }
    public void PushEntityWithAngle(float pushForce, Vector3 baseDirection, float angle, float duration = 0f, Ease easeType = Ease.Linear, float delay = 0f, Action onComplete = null)
    {
        Vector3 rotatedDirection = Quaternion.Euler(0f, 0f, angle) * baseDirection.normalized;
        Vector3 newPosition = EntityTransform.position + rotatedDirection * pushForce;
        EntityTransform.DOMove(newPosition, duration).SetEase(easeType).SetDelay(delay).OnComplete(() => onComplete?.Invoke());
    }
}
