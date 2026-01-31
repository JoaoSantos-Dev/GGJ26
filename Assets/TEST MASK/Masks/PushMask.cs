using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PushMask : MaskBase
{
    [SerializeField] private float pushForce = 10f;
    [Tooltip("The maximum distance an enemy can be to the hability to take effect")]
    [SerializeField] private float effectDistance = 10f;

    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;
    public override event Action OnHabilityEnd;

    private void Awake()
    {
        MaskType = MaskType.Push;
    }
    public override void UseMaskHability()
    {
        base.UseMaskHability();

        
        if (collider == null) collider = gameObject.AddComponent<BoxCollider2D>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Vector2 direction = playerMovementHandler.CurrentMovementDirection.normalized;
        transform.up = direction;

        Vector2 thickness = new Vector2(2f, 2f);
        Vector2 colliderSize = collider.size = new Vector2(thickness.x, effectDistance);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector3 scale = transform.localScale;
        scale.x = colliderSize.x / spriteSize.x;
        scale.y = colliderSize.y / spriteSize.y;

        spriteRenderer.transform.localScale = scale;
        collider.offset = Vector2.up * (effectDistance / 2f);
        spriteRenderer.transform.localPosition = new Vector3(spriteRenderer.transform.localPosition.x, collider.offset.y, spriteRenderer.transform.localPosition.z);
        collider.enabled = true;
        collider.isTrigger = true;
        OnHabilityEnd?.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Correct line. Uncomment when the effect collider problem has been solved.
        //if (!collision.TryGetComponent(out EntityBase entity)) return;

        if (!collision.TryGetComponent(out EnemyController entity)) return;
        if (collision.transform == transform) return;

        Vector2 closestPoint = collider.ClosestPoint(collision.bounds.center);
        Vector2 localPoint = transform.InverseTransformPoint(closestPoint);

        BoxCollider2D box = collider as BoxCollider2D;
        if (box == null) return;

        Vector2 halfSize = box.size * 0.5f;

        float xNorm = localPoint.x / halfSize.x;
        float yNorm = localPoint.y / halfSize.y;

        Rigidbody2D rb = collision.attachedRigidbody;
        if (rb == null) return;
        Debug.Log($"LocalPoint: {localPoint} | xNorm: {xNorm:F2} | yNorm: {yNorm:F2}");
        if (yNorm > 1 + 0.6f)
        {
            entity.MovementHandler.PushEntity(pushForce, playerMovementHandler.CurrentMovementDirection.normalized, 0.5f);
            Debug.Log("Top of the collider");
        }
        else if (xNorm > 0.1f)
        {
            entity.MovementHandler.PushEntityWithAngle(pushForce, playerMovementHandler.CurrentMovementDirection.normalized, 45f, 0.5f);
            Debug.Log("Right Side of the collider");
        }
        else if (xNorm < -0.1f)
        {
            entity.MovementHandler.PushEntityWithAngle(pushForce, playerMovementHandler.CurrentMovementDirection.normalized, -45f, 0.5f);
            Debug.Log("Left Side of the collider");
        }
    }
}
