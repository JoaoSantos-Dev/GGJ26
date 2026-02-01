using System;
using DG.Tweening;

public class Cooldown 
{
    private bool IsRunning => tween != null && tween.IsActive() && tween.IsPlaying();
    private bool IsPaused => tween != null && tween.IsActive() && !tween.IsPlaying();
    public bool IsReady => RemainingTime <= 0f && !IsPaused;
    public float Duration { get; private set; }
    public float RemainingTime { get; private set; }
    

    private Tween tween;

    public event Action<float> Updated;

    public void Prepare(float duration, Action onCooldownComplete = null)
    {
        if (IsRunning || IsPaused) return;
        Start(duration, onCooldownComplete);
        Pause();
    }
    public void Start(float duration, Action onCooldownComplete = null)
    {
        if (IsRunning) return;

        if (IsPaused)
        {
            Resume();
            return;
        }

        Reset();

        Duration = duration;
        RemainingTime = duration;

        tween = DOTween.To(() => RemainingTime, value => RemainingTime = value, 0f, duration).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Reset();
                onCooldownComplete?.Invoke();
                UnityEngine.Debug.Log("Cooldown Ready");
            })
            .SetUpdate(false)
            .OnUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        Updated?.Invoke(RemainingTime);
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
