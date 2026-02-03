using System;
using Core;
using UnityEngine;

public class PushMask : MaskBase
{
    [SerializeField,Min(1)] private float pushForce = 10f;
    [Tooltip("The maximum distance an enemy can be to the hability to take effect")]
    [SerializeField,Min(1)] private float effectDistance = 10f;
    // private BoxCollider2D collider;
    // private SpriteRenderer spriteRenderer;
    [SerializeField, Range(1,360)] float angle = 60;
    [SerializeField] private LayerMask targetLayer;
    private Collider2D[] pushObjects = new Collider2D[16];
    public override event Action OnHabilityEnd;

    public Vector2 GetDirection()
    {
        if (playerMovementHandler != null)
        {
            return playerMovementHandler.LastMovementDirection;
        }
        else
        {
            return Vector2.zero;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        MaskType = MaskType.Push;
    }
    
    public override void UseMaskHability()
    {
        base.UseMaskHability();

        // --- JOÃO: SOM DE DASH AQUI ---
        if (AudioManager.Instance != null && AudioManager.Instance.listaDeSons != null)
        {
            // Toca o som de Dash na posição atual do jogador
            AudioManager.Instance.PlayEffect(AudioManager.Instance.listaDeSons.maskPush, transform.position);
        }
        // ------------------------------
        TryPushObjects();
        //
        // if (collider == null) collider = gameObject.AddComponent<BoxCollider2D>();
        //
        // // spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // Vector2 direction = playerMovementHandler.CurrentMovementDirection.normalized;
        // transform.up = direction;
        //
        // Vector2 thickness = new Vector2(2f, 2f);
        // Vector2 colliderSize = collider.size = new Vector2(thickness.x, effectDistance);
        // Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        //
        // Vector3 scale = transform.localScale;
        // scale.x = colliderSize.x / spriteSize.x;
        // scale.y = colliderSize.y / spriteSize.y;
        //
        // spriteRenderer.transform.localScale = scale;
        // collider.offset = Vector2.up * (effectDistance / 2f);
        // spriteRenderer.transform.localPosition = new Vector3(spriteRenderer.transform.localPosition.x, collider.offset.y, spriteRenderer.transform.localPosition.z);
        // collider.enabled = true;
        // collider.isTrigger = true;
        OnHabilityEnd?.Invoke();
    }

    

    private void TryPushObjects()
    {
        pushObjects = Physics2D.OverlapCircleAll(transform.position, effectDistance, targetLayer);
        if (pushObjects.Length <= 0) return;
        foreach (var collider2D in pushObjects)
        {
            var targetPosition = collider2D.transform.position;
            var targetDirection = (targetPosition - transform.position).normalized;
            Vector2 center = transform.position;
            bool targetInsideArc = center.IsInsideArc(targetPosition, 
                GetDirection(), 
                effectDistance, 
                angle);
            if ( targetInsideArc)
            {
                if (!collider2D.TryGetComponent(out EntityBase entity)) return;
                entity.MovementHandler.PushEntity(pushForce,targetDirection, 0.5f);
                
            }
        }

    }
    

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     //Correct line. Uncomment when the effect collider problem has been solved.
    //     //if (!collision.TryGetComponent(out EntityBase entity)) return;
    //
    //     if (!collision.TryGetComponent(out EnemyController entity)) return;
    //     if (collision.transform == transform) return;
    //
    //     Vector2 closestPoint = collider.ClosestPoint(collision.bounds.center);
    //     Vector2 localPoint = transform.InverseTransformPoint(closestPoint);
    //
    //     BoxCollider2D box = collider as BoxCollider2D;
    //     if (box == null) return;
    //
    //     Vector2 halfSize = box.size * 0.5f;
    //
    //     float xNorm = localPoint.x / halfSize.x;
    //     float yNorm = localPoint.y / halfSize.y;
    //
    //     Rigidbody2D rb = collision.attachedRigidbody;
    //     if (rb == null) return;
    //     Debug.Log($"LocalPoint: {localPoint} | xNorm: {xNorm:F2} | yNorm: {yNorm:F2}");
    //     if (yNorm > 1 + 0.6f)
    //     {
    //         entity.MovementHandler.PushEntity(pushForce, playerMovementHandler.CurrentMovementDirection.normalized, 0.5f);
    //         Debug.Log("Top of the collider");
    //     }
    //     else if (xNorm > 0.1f)
    //     {
    //         entity.MovementHandler.PushEntityWithAngle(pushForce, playerMovementHandler.CurrentMovementDirection.normalized, 45f, 0.5f);
    //         Debug.Log("Right Side of the collider");
    //     }
    //     else if (xNorm < -0.1f)
    //     {
    //         entity.MovementHandler.PushEntityWithAngle(pushForce, playerMovementHandler.CurrentMovementDirection.normalized, -45f, 0.5f);
    //         Debug.Log("Left Side of the collider");
    //     }
    // }

    private void OnDrawGizmos()
    {
        if (playerMovementHandler == null) return;
        Vector2 inputDirection = playerMovementHandler.LastMovementDirection.normalized;
        Vector3 leftDir  = Quaternion.Euler(0, 0, +angle * 0.5f) * inputDirection;
        Vector3 rightDir = Quaternion.Euler(0, 0, -angle * 0.5f) * inputDirection;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftDir * effectDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightDir * effectDistance);
        Gizmos.DrawLine(transform.position, transform.position +(Vector3) inputDirection*effectDistance);
    }


    
    
}
