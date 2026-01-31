using System;
using DG.Tweening;

public class Cooldown 
{
    private bool IsRunning => tween != null && tween.IsActive() && tween.IsPlaying();
    private bool IsPaused => tween != null && tween.IsActive() && !tween.IsPlaying();
    public bool IsReady => RemainingTime <= 0f && !IsPaused;
    public float RemainingTime { get; private set; }

    private Tween tween;

    public void Start(float duration, Action onCooldownComplete = null)
    {
        if (IsRunning) return;

        if (IsPaused)
        {
            Resume();
            return;
        }

        Reset();

        RemainingTime = duration;

        tween = DOTween.To(() => RemainingTime,value => RemainingTime = value, 0f, duration).SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            Reset();
            onCooldownComplete?.Invoke();
            UnityEngine.Debug.Log("Cooldown Ready");
        });
    }
    public void Pause()
    {
        if (IsRunning) tween.Pause();
    }

    public void Resume()
    {
        if (IsPaused) tween.Play();
    }

    public void Reset()
    {
        if (tween != null)
        {
            tween.Kill();
            tween = null;
        }

        RemainingTime = 0f;
    }
}
