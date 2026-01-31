using DG.Tweening;
using UnityEngine;

public class AnimationController
{
    private Transform characterTransform;
    private Vector3 initialScale;
    public SpriteRenderer SpriteRenderer { get; private set; }
    public SpriteRenderer MaskRenderer { get; private set; }

    private Tween tween;
    public AnimationController(SpriteRenderer SpriteRenderer, SpriteRenderer maskRenderer )
    {
        this.SpriteRenderer = SpriteRenderer;
        this.MaskRenderer = maskRenderer;
        characterTransform = SpriteRenderer.transform.parent;
        initialScale = characterTransform.lossyScale;
    }

    public void StartMoveAnimation()
    {
        tween.Kill();
        tween = characterTransform.DOScaleY(initialScale.y * 1.2f, 0.1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    public void StopMoveAnimation()
    {
        tween.Kill();
        tween = characterTransform.DOScale(initialScale, 0.1f).SetEase(Ease.InOutQuad);
    }
}
