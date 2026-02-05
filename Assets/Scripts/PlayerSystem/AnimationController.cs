using DG.Tweening;
using UnityEngine;

public class AnimationController
{
    private Transform characterTransform;
    private Vector3 initialScale;
    public SpriteRenderer SpriteRenderer { get; private set; }
    public SpriteRenderer MaskRenderer { get; private set; }
    public SpriteRenderer HeadRenderer { get; private set; }
    public Animator Animator { get; private set; }

    private Tween tween;
    public AnimationController(SpriteRenderer SpriteRenderer, SpriteRenderer maskRenderer, SpriteRenderer headRenderer, Animator animator )
    {
        this.SpriteRenderer = SpriteRenderer;
        this.MaskRenderer = maskRenderer;
        this.HeadRenderer = headRenderer;
        this.Animator = animator;
        characterTransform = SpriteRenderer.transform.parent;
        initialScale = characterTransform.lossyScale;
    }

    public void StartMoveAnimation()
    {
        tween.Kill();
        tween = characterTransform.DOScaleY(initialScale.y * 1.2f, 0.1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        Animator.Play(UnityEngine.Animator.StringToHash("Move"));   
    }

    public void StopMoveAnimation()
    {
        tween.Kill();
        tween = characterTransform.DOScale(initialScale, 0.1f).SetEase(Ease.InOutQuad);
        Animator.Play(UnityEngine.Animator.StringToHash("Idle"));
    }

    public void SetFlip(bool flip)
    {
        SpriteRenderer.flipX = flip;
        MaskRenderer.flipX = !flip;
        HeadRenderer.flipX = flip;
    }

    public void PlayDeath()
    {
        Animator.Play(UnityEngine.Animator.StringToHash("Death"));
    }

    public void PlayStun()
    {
        Animator.Play(UnityEngine.Animator.StringToHash("Stun"));
    }
}
