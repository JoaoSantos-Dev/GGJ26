
using System;
using DG.Tweening;
using GameplaySystem.AI;
using Playersystem;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : EntityBase
{
    [SerializeField] private float animPulseFrequency = 0.1f;
    [SerializeField, Min(1)] private int damage = 10;
     private SpriteRenderer spriteRenderer;
     
    private Vector3 initialScale;
     private Tween animationTween;
    private FollowerEnemyAI followerAI;
    private void Awake()
    {
        initialScale = transform.lossyScale;
        MovementHandler = new EnemyMovementHandler(transform);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartMoveAnimation();

    }

    private void OnDisable()
    {
        StopMoveAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.TryGetComponent(out PlayerController player))
            {
                player.TakeDamage(damage);
            }
        }
    }

    public void StartMoveAnimation()
    {
        animationTween.Kill();
        animationTween = transform.DOScaleY(initialScale.y * 1.2f, animPulseFrequency).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    public void StopMoveAnimation()
    {
        animationTween.Kill();
        animationTween = transform.DOScale(initialScale, animPulseFrequency).SetEase(Ease.InOutQuad);
    }

    public void SetRendererFlip(bool value)
    {
        spriteRenderer.flipX = value;   
        
    }

    public void SetScale(Vector3 scale, float duration = 0.1f)
    {
        animationTween.Kill();
        transform.localScale = initialScale;
        animationTween = transform.DOScale(scale, duration).SetEase(Ease.InOutQuad);
    }
    public void SetScaleY(float value, float duration = 0.1f)
    {
        animationTween.Kill();
        transform.localScale = initialScale;
        animationTween = transform.DOScaleY(value, duration).SetEase(Ease.InOutQuad);
    }
    public void SetScaleX(float value, float duration = 0.1f)
    {
        animationTween.Kill();
        transform.localScale = initialScale;
        animationTween = transform.DOScaleX(value, duration).SetEase(Ease.InOutQuad);
    }
}
