using System;
using DG.Tweening;
using UnityEngine;

public class ShieldMask : MaskBase
{
    [SerializeField] private float shieldRadius = 2f;
    [SerializeField] private float shieldDuration = 2f;
    [Tooltip("The total time to the shiled to grow to its maximum size")]
    [SerializeField] private float defaultCastSpeed = 0.5f;
    [SerializeField] private float pushForce = 0.5f;

    private CircleCollider2D collider;
    private SpriteRenderer spriteRenderer;

    public override event Action OnHabilityEnd;
    public override void UseMaskHability()
    {
        base.UseMaskHability();

        if (collider == null) collider = gameObject.AddComponent<CircleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        collider.radius = 0f;
        collider.isTrigger = true;
        collider.enabled = true;
        spriteRenderer.transform.localScale = Vector3.zero;
        spriteRenderer.transform.DOScale(shieldRadius * 2, defaultCastSpeed);
        DOTween.To(() => collider.radius, endValue => collider.radius = endValue, shieldRadius, defaultCastSpeed).SetUpdate(UpdateType.Fixed).OnComplete(() =>
        {
        });

        OnHabilityEnd?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Correct line.Uncomment when the effect collider problem has been solved.
        //if (!collision.TryGetComponent(out EntityBase entity)) return;

        if (!collision.TryGetComponent(out EnemyController entity)) return;
        if (collision.transform == transform) return;
        Vector3 direction = entity.transform.position - transform.position;
        entity.MovementHandler.PushEntity(pushForce, direction.normalized, 0.5f);
    }
}
